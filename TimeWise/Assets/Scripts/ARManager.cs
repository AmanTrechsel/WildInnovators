using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

public class ARManager : MonoBehaviour
{
  public static ARManager Instance;
  public Camera arCamera;
  public GameObject nameTagPrefab;
  public Vector3 trackedImagePosition = Vector3.zero;
  public bool trackedImageVisible = false;
  [SerializeField]
  private GameObject arCursor;
  [SerializeField]
  private ARTrackedImageManager imageTracking;
  private GameObject arTrackedObject;

  private void Awake()
  {
    if (Instance == null) { Instance = this; }
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

  public void TakePicture()
  {
    // Initialize and render
    Texture2D photo = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
    RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
    arCamera.targetTexture = renderTexture;
    arCamera.Render();
    RenderTexture.active = renderTexture;
    
    // Read pixels
    photo.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
    
    // Clean up
    arCamera.targetTexture = null;
    RenderTexture.active = null;
    DestroyImmediate(renderTexture);

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

  public void BackToSelection()
  {
    AppManager.Instance.LoadScene("Selection");
  }
}
