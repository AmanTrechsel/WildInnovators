using System.IO;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

public class ARManager : MonoBehaviour
{
  // Singleton
  public static ARManager Instance;

  // Cameras for the AR and screen
  public Camera screenCamera, arCamera;
  // The prefab for the nametag
  public GameObject nameTagPrefab;
  // The position of the tracked image (ARCard)
  public Vector3 trackedImagePosition = Vector3.zero;
  // Whether or not the tracked image is visible
  public bool trackedImageVisible = false;
  // Whether or not the user is calibrating
  public bool calibrating;

  // The AR Cursor
  [SerializeField]
  private GameObject arCursor;
  // Reference to the tooltip object
  [SerializeField]
  private GameObject tooltip;
  // The AR Tracked Image Manager
  [SerializeField]
  private ARTrackedImageManager imageTracking;
  // The AR Tracked Object
  private GameObject arTrackedObject;
  // Layouts for calibration and mask
  [SerializeField]
  private GameObject calibrationLayout, noCalibrationLayout, calibrationMask;
  // Current subject name text field
  [SerializeField]
  private TextMeshProUGUI currentSubjectName;
  // Encyclopedia unlock count text field
  [SerializeField]
  private TextMeshProUGUI encyclopediaUnlockCount;

  // Called when the script instance is being loaded
  private void Awake()
  {
    // Check if the instance exists and if not, set it to this
    if (Instance == null) { Instance = this; }
    else if (Instance != this) { Destroy(gameObject); }

    // Hide the calibration layout
    HideCalibration();

    // Set the current subject name
    currentSubjectName.text = AppManager.Instance.arSubject.name;

    // Set the encyclopedia unlock count
    encyclopediaUnlockCount.text = AppManager.Instance.GetEncyclopediaUnlockText();

    // Hide the tooltip if the user has already clicked an object
    if (AppManager.Instance.hasClicked) { HideTooltip(); }
  }

  // Update is called once per frame
  private void Update()
  {
    // Try to find the ARCardPosition object and set the tracked image position to it
    if (arTrackedObject == null) { arTrackedObject = GameObject.Find("ARCardPosition(Clone)"); }
    else { trackedImagePosition = arTrackedObject.transform.position; }
  }

  // Called when the object becomes enabled and active
  private void OnEnable()
  {
    // Subscribe to the tracked image changed event
    imageTracking.trackedImagesChanged += TrackedImageChange;
  }

  // Called when the object becomes disabled and inactive
  private void OnDisable()
  {
    // Unsubscribe from the tracked image changed event
    imageTracking.trackedImagesChanged -= TrackedImageChange;
  }

  // Called when the tracked image changes
  // source: https://docs.unity3d.com/Packages/com.unity.xr.arfoundation@4.0/manual/tracked-image-manager.html
  private void TrackedImageChange(ARTrackedImagesChangedEventArgs eventArgs)
  {
    // When the tracked image is added or updated, update the tracked image otherwise make it invisible
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

  // Update the tracked image
  private void UpdateTrackedImage(ARTrackedImage trackedImage)
  {
    // Reposition the AR Cursor to the tracked image and set it to visible
    arCursor.transform.position = trackedImage.transform.position;
    trackedImageVisible = true;
  }

  // Hides the tooltip
  public void HideTooltip()
  {
    tooltip.SetActive(false);
  }

  // Show the calibration layout
  public void ShowCalibration()
  {
    // Enable the calibration layout and disable the no calibration layout
    calibrating = true;
    calibrationMask.SetActive(false);
    calibrationLayout.SetActive(true);
    noCalibrationLayout.SetActive(false);
  }

  // Hide the calibration layout
  public void HideCalibration()
  {
    // Disable the calibration layout and enable the no calibration layout
    calibrating = false;
    calibrationMask.SetActive(false);
    calibrationLayout.SetActive(false);
    noCalibrationLayout.SetActive(true);
  }

  // Basic method for opening the encyclopedia
  public void OpenEncyclopedia()
  {
    // Open the encyclopedia
    AppManager.Instance.GoToEncyclopedia();
  }

  // Add the calibration data to the AR Session
  public void Calibrate()
  {
    // Check if the AR Session is initializing or tracking. If not, return
    if (!(ARSession.state == ARSessionState.SessionInitializing || ARSession.state == ARSessionState.SessionTracking)) { return; }

    // Take a screenshot of the AR Camera
    Texture2D calibrationData = Screenshot(arCamera);
    calibrationMask.SetActive(true);

    // Enable the screen camera
    screenCamera.enabled = true;
    
    // Take a screenshot of the screen camera
    Texture2D calibrationImageMask = Screenshot(screenCamera);
    calibrationMask.SetActive(false);

    // Disable the screen camera
    screenCamera.enabled = false;

    // Loop through the pixels of the calibration data and set the pixels to clear if the pixel is black in the calibration image mask
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

    // Apply the changes to the calibration data
    var library = imageTracking.CreateRuntimeLibrary();
    if (library is MutableRuntimeReferenceImageLibrary mutableLibrary)
    {
      mutableLibrary.ScheduleAddImageWithValidationJob(calibrationData, "calibration data", 0.5f);
    }
    AppManager.Instance.calibrationData.Add(calibrationData);

    // Hide the calibration layout
    HideCalibration();
  }

  // Take a picture with the AR Camera
  public void TakePicture()
  {
    // Take a screenshot of the AR Camera
    Texture2D photo = Screenshot(arCamera);

    // Get directory for picture
    System.DateTime dt = System.DateTime.Now;
    dt = dt.Add(System.TimeSpan.FromSeconds(180));
    string path = $"{AppManager.Instance.GetAndroidExternalStoragePath()}/{SettingsManager.Instance.GetLocationName()}";
    string filename = $"{dt.ToString().Replace(":", "_").Replace(" ", "_").Replace("/", "_")}.png";

    // Create path if it doesn't exist yet
    if (!Directory.Exists(path))
    {    
      Directory.CreateDirectory(path);
    }

    // Make screenshot
    byte[] bytes = photo.EncodeToPNG();
    File.WriteAllBytes($"{path}/{filename}", bytes);
  }

  // Take a screenshot of the given camera
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

    // Return the photo
    return photo;
  }

  // Go back to the selection scene
  public void BackToSelection()
  {
    AppManager.Instance.LoadScene("Selection");
  }
}
