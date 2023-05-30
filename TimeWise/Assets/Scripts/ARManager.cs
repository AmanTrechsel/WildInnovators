using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARManager : MonoBehaviour
{
  public static ARManager Instance;
  public Camera screenCamera, arCamera;
  public GameObject nameTagPrefab;
  public Vector3 trackedImagePosition = Vector3.zero;
  public bool trackedImageVisible = false;
  public bool calibrating;
  [SerializeField]
  private GameObject arCursor;
  [SerializeField]
  private ARTrackedImageManager imageTracking;
  private GameObject arTrackedObject;
  [SerializeField]
  private GameObject calibrationLayout, noCalibrationLayout, calibrationMask;

  private void Awake()
  {
    if (Instance == null) { Instance = this; }
    else if (Instance != this) { }//Destroy(gameObject); }

    HideCalibration();
  }

  private void Update()
  {
    if (arTrackedObject == null) { arTrackedObject = GameObject.Find("ARCardPosition(Clone)"); }
    else { trackedImagePosition = arTrackedObject.transform.position; }
  }

  private void OnEnable()
  {
    imageTracking.trackedImagesChanged += TrackedImageChange;
  }

  private void OnDisable()
  {
    imageTracking.trackedImagesChanged -= TrackedImageChange;
  }

  private void TrackedImageChange(ARTrackedImagesChangedEventArgs eventArgs)
  {
    foreach (ARTrackedImage trackedImage in eventArgs.added)
    {
      UpdateTrackedImage(trackedImage);
    }
    foreach (ARTrackedImage trackedImage in eventArgs.updated)
    {
      UpdateTrackedImage(trackedImage);
    }
    foreach (ARTrackedImage trackedImage in eventArgs.removed)
    {
      trackedImageVisible = false;
    }
  }

  private void UpdateTrackedImage(ARTrackedImage trackedImage)
  {
    arCursor.transform.position = trackedImage.transform.position;
    trackedImageVisible = true;
  }

  public void ShowCalibration()
  {
    calibrating = true;
    calibrationMask.SetActive(false);
    calibrationLayout.SetActive(true);
    noCalibrationLayout.SetActive(false);
  }

  public void HideCalibration()
  {
    calibrating = false;
    calibrationMask.SetActive(false);
    calibrationLayout.SetActive(false);
    noCalibrationLayout.SetActive(true);
  }

  public void Calibrate()
  {
    Texture2D calibrationData = Screenshot(arCamera);
    calibrationMask.SetActive(true);
    
    Texture2D calibrationImageMask = Screenshot(screenCamera);
    calibrationMask.SetActive(false);

    for (int x = 0; x < Screen.width; x++)
    {
      for (int y = 0; y < Screen.height; y++)
      {
        if (calibrationImageMask.GetPixel(x, y) == Color.black)
        {
          calibrationData.SetPixel(x, y, Color.clear);
        }
      }
    }

    //if (!(ARSession.state == ARSessionState.SessionInitializing || ARSession.state == ARSessionState.SessionTracking)) { return; }

    var library = imageTracking.CreateRuntimeLibrary();
    if (library is MutableRuntimeReferenceImageLibrary mutableLibrary)
    {
      mutableLibrary.ScheduleAddImageWithValidationJob(calibrationData, "calibration data", 0.5f);
    }
    AppManager.Instance.calibrationData.Add(calibrationData);
    HideCalibration();
  }

  public void TakePicture()
  {
    Texture2D photo = Screenshot(arCamera);

    // Get directory for picture
    System.DateTime dt = System.DateTime.Now;
    dt = dt.Add(System.TimeSpan.FromSeconds(180));
    string path = $"{AppManager.Instance.GetAndroidExternalStoragePath()}/Timewise";
    string filename = $"{dt.ToString().Replace(":", "_").Replace(" ", "_").Replace("/", "_")}.png";

    // Create path if it doesn't exist yet
    if(!Directory.Exists(path))
    {    
      Directory.CreateDirectory(path);
    }

    // Make screenshot
    byte[] bytes = photo.EncodeToPNG();
    File.WriteAllBytes($"{path}/{filename}", bytes);
  }

  public Texture2D Screenshot(Camera camera)
  {
    // Initialize and render
    Texture2D photo = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
    RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
    camera.targetTexture = renderTexture;
    camera.Render();
    RenderTexture.active = renderTexture;
    
    // Read pixels
    photo.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
    
    // Clean up
    camera.targetTexture = null;
    RenderTexture.active = null;
    DestroyImmediate(renderTexture);

    return photo;
  }

  public void BackToSelection()
  {
    AppManager.Instance.LoadScene("Selection");
  }
}
