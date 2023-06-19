using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class AppManager : MonoBehaviour
{
  // Whether the user has agreed to understanding the warning
  public bool warningAgreed;

  // Data to be saved
  public bool permission = true;
  public int languageIndex;
  public string location;
  public bool hasClicked;
  public List<Texture2D> calibrationData = new List<Texture2D>();

  // Singleton
  public static AppManager Instance;

  // App Canvas that is static throughout the app
  [SerializeField]
  private GameObject appCanvas;
  // Search button
  [SerializeField]
  private GameObject searchButton;
  // No permission popup
  [SerializeField]
  private GameObject noPermissionPopup;
  // Load bar
  [SerializeField]
  private Image loadBar;

  // The currently selected course
  [HideInInspector]
  public Course selectedCourse;
  // The currently selected subject
  public Subject arSubject;
  // All encyclopedia pages that have been unlocked
  public List<int> unlockedEncyclopediaPages = new List<int>();
  // Scene that was active before the current one
  private string previousScene;

  // Called once at the start of the app
  private void Awake()
  {
    // Assign this instance as the singleton
    if (Instance == null) { Instance = this; }
    else if (Instance != this) { Destroy(gameObject); }

    // Ensure this object remains active between scenes
    DontDestroyOnLoad(gameObject);

    // Ensure the no permission popup is hidden
    HideNoPermissionPopup();
  }

  // Gets the Android storage path (source: https://stackoverflow.com/questions/60475027/unity-android-save-screenshot-in-gallery)
  public string GetAndroidExternalStoragePath()
  {
    if (Application.platform != RuntimePlatform.Android) { return Application.persistentDataPath; }
    var jc = new AndroidJavaClass("android.os.Environment");
    var path = jc.CallStatic<AndroidJavaObject>("getExternalStoragePublicDirectory", jc.GetStatic<string>("DIRECTORY_DCIM")).Call<string>("getAbsolutePath");
    return path;
  }

  // Basic method for loading a scene
  public void LoadScene(string sceneName)
  {
    // Set the previous scene to the current scene
    previousScene = SceneManager.GetActiveScene().name;
    // Swap the scene
    StartCoroutine(LoadSceneAsync(sceneName));
  }

  // Returns a string of the number of encyclopedia pages unlocked
  public string GetEncyclopediaUnlockText()
  {
    return $"{AppManager.Instance.unlockedEncyclopediaPages.Count}/{ResourceManager.Instance.encyclopediaPages.Count}";
  }

  // Load the scene asynchronously
  private IEnumerator LoadSceneAsync(string sceneName)
  {
    // The Application loads the Scene in the background as the current Scene runs.
    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

    // Wait until the asynchronous scene fully loads
    while (!asyncLoad.isDone)
    {
      // Update load bar based on loading progress
      loadBar.fillAmount = asyncLoad.progress;
      yield return null;
    }

    // The scene has successfully loaded

    // Show or hide the AppCanvas based on the scene being loaded
    if (sceneName == "ARWarning") { appCanvas.SetActive(false); }
    else { appCanvas.SetActive(true); }
    // Show or hide the SearchButton based on the scene being loaded
    if (sceneName == "CourseSelect" || sceneName == "Selection") { searchButton.SetActive(true); }
    else { searchButton.SetActive(false); }

    // Hide the load bar
    loadBar.fillAmount = 0f;
  }

  // Updates every frame
  private void Update()
  {
    // Check if the app is opened on an Android
    if (Application.platform == RuntimePlatform.Android)
    {
      // Check if back button is pressed
      if (Input.GetKey(KeyCode.Escape))
      {
        // Check if the no permission popup is active
        if (noPermissionPopup.activeSelf)
        {
          // Hide the no permission popup and return
          CloseNoPermissionPopup();
          return;
        }

        // Goes to the previous scene
        GoBack();
      }
    }
  }

  // Goes to the previous scene
  public void GoBack()
  {
    // Set the scene to load to an empty string
    string sceneToLoad = "";

    // Check which scene is currently active and set the scene to load accordingly
    switch (SceneManager.GetActiveScene().name)
    {
      case "CourseSelect":
        break;
      case "Selection":
        sceneToLoad = "CourseSelect";
        break;
      case "SelectionSearch":
        sceneToLoad = "Selection";
        break;
      case "ARWarning":
        sceneToLoad = "Selection";
        break;
      case "AR":
        sceneToLoad = "Selection";
        break;
      case "Encyclopedia":
        sceneToLoad = previousScene;
        break;
      case "Settings":
        sceneToLoad = previousScene;
        break;
      case "Code":
        sceneToLoad = "CourseSelect";
        break;
    }

    // Check if the scene to load is not empty and load it
    if (sceneToLoad != "")
    {
      LoadScene(sceneToLoad);
      return;
    }
    //if (previousScene != null)
    //{
    //  AppManager.Instance.LoadScene(previousScene);
    //  return;
    //}
  }

  // Show the no permission popup
  private void ShowNoPermissionPopup()
  {
    noPermissionPopup.SetActive(true);
  }

  // Hide the no permission popup
  private void HideNoPermissionPopup()
  {
    noPermissionPopup.SetActive(false);
  }

  // Method for the no permission popup
  public void CloseNoPermissionPopup()
  {
    HideNoPermissionPopup();
    LoadScene("Settings");
  }

  // Send the user to the AR Scene based on parameters
  public void GoToARScene()
  {
    if(AppManager.Instance.permission == true)
    {
      LoadScene("ARWarning");
    }
    else
    {
      ShowNoPermissionPopup();
    }
  }

  // Change the current scene
  public void ChangeScene(string newScene)
  {
    LoadScene(newScene);
  }

  // Convert a larger number into a normal format
  private string LargeNumberToDisplayString(float largeNumber)
  {
    double number = (double)largeNumber;
    string suffix = "";
    if (number >= 1e12)
    {
      number /= 1e12;
      suffix = " Biljoen";
    }
    else if (number >= 1e9)
    {
      number /= 1e9;
      suffix = " Miljard";
    }
    else if (number >= 1e6)
    {
      number /= 1e6;
      suffix = " Miljoen";
    }

    return number.ToString(number >= 10 ? "0.#" : "0.##") + suffix;
  }

  // Method for the settingsbutton
  public void GoToSettings()
  {
    LoadScene("Settings");
  }

  // Method for getting to the Encyclopedia
  public void GoToEncyclopedia()
  {
    LoadScene("Encyclopedia");
  }

  // Method for going to the home page
  public void GoToHome()
  {
    LoadScene("CourseSelect");
  }

  // Opens the code input menu
  public void OpenCodeInputMenu()
  {
    LoadScene("Code");
  }

  // Method to open URLs
  public void OpenURL(string url)
  {
    Application.OpenURL(url);
  }

}
