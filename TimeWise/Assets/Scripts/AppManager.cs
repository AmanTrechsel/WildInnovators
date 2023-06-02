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
  public List<Texture2D> calibrationData = new List<Texture2D>();

  // Singleton
  public static AppManager Instance;

  // App Canvas that is static throughout the app
  [SerializeField]
  private GameObject appCanvas;

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
    SceneManager.LoadScene(sceneName);
    // Show or hide the AppCanvas based on the scene being loaded
    if (sceneName == "ARWarning") { appCanvas.SetActive(false); }
    else { appCanvas.SetActive(true); }
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
        string sceneToLoad = "";
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
        }

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
    }
  }

  // Send the user to the AR Scene based on parameters
  public void GoToARScene()
  {
    if(AppManager.Instance.permission == true)
    {
      LoadScene("ARWarning");

      foreach (EncyclopediaPage encyclopediaPage in ResourceManager.Instance.GetEncyclopediaPagesBySubject(arSubject))
      {
        unlockedEncyclopediaPages.Add((int)encyclopediaPage.id);
      }
    }
    else
    {
      Debug.Log("Er is geen toestemming gegeven om de camera te gebruiken");
      // Needs to be changed to something in the UI instead of a console message
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

  // Method to open URLs
  public void OpenURL(string url)
  {
    Application.OpenURL(url);
  }

}
