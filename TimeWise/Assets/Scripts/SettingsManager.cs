using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using TMPro;
using UnityEngine.Localization.Settings;
using System.IO;

public class SettingsManager : MonoBehaviour
{
  // Singleton
  public static SettingsManager Instance;

  // Name of the save file in which data will de saved
  // .json isn't the most secure format, might want to change it to a binary file or something like that.
  private string saveFile = "time.wise";

  // Assign this instance as the singleton
  private void Awake()
  {
    if (Instance == null) { Instance = this; }
    else if (Instance != this) { Destroy(gameObject); }

    // Load the settings from the local file
    LoadData();
  }

  // Method for saving the settings on a local file
  public void SaveData()
  {
    SettingsData data = new SettingsData();
    data.permission = AppManager.Instance.permission;
    data.languageIndex = AppManager.Instance.languageIndex;
    data.location = AppManager.Instance.location;
    data.hasClicked = AppManager.Instance.hasClicked;
    
    string json = JsonUtility.ToJson(data);
    File.WriteAllText(Application.dataPath + "/" + saveFile, json);
  }

  // Method for loading the settings from a local file
  public void LoadData()
  {
    string json = File.ReadAllText(Application.dataPath + "/" + saveFile);
    SettingsData data = JsonUtility.FromJson<SettingsData>(json);

    AppManager.Instance.permission = data.permission;
    AppManager.Instance.languageIndex = data.languageIndex;
    AppManager.Instance.location = data.location;
    AppManager.Instance.hasClicked = data.hasClicked;
  }

  // For the permission setting. The parameters are given through the separate PermissionManager.cs script
  public void SwitchPermission(Button button, Sprite toggleOff, Sprite toggleOn)
  {
    // Check the current state of the button and switch it accordingly
    if (button.image.sprite == toggleOn)
    {
      button.image.sprite = toggleOff;
      AppManager.Instance.permission = false;
    }
    else if (button.image.sprite == toggleOff)
    {
      // If the permission is not yet given, ask for it
      if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
      {
        // The permission is asked for
        Permission.RequestUserPermission(Permission.Camera);

        // If the permission is still not given, return
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
          return;
        }
      }

      // Actually switch the permission
      button.image.sprite = toggleOn;
      AppManager.Instance.permission = true;
    }
  }

  // For the language setting. The parameter is given through the separate LanguageManager.cs script
  public void ChangeLanguage(int ID)
  {
    StartCoroutine(SetLanguage(ID));
    AppManager.Instance.languageIndex = ID;
  }

  IEnumerator SetLanguage(int index)
  {
    yield return LocalizationSettings.InitializationOperation;
    LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
  }

  // For the location setting. The parameters are given through the separate LocationManager.cs script
  private string locationName = "Timewise";

  public void ChangeLocation(GameObject inputField, string location)
  {
    if (inputField.GetComponent<TMP_InputField>().text != location)
    {
      if (inputField.GetComponent<TMP_InputField>().text == "")
      {
        inputField.GetComponent<TMP_InputField>().text = "Timewise";
      }
      
      location = inputField.GetComponent<TMP_InputField>().text;
      locationName = location;
      AppManager.Instance.location = locationName;
    }
  }

  // Getter for the inputfield
  public string GetLocationName()
  {
    return locationName;
  }

  // Method for the privacy policy button
  public void OpenPrivacyPolicy()
  {
    AppManager.Instance.OpenURL("https://timewise.serverict.nl/policy.html");
  }

  // Pop-up settings. Makes the setting-overlay pop up when the setting is pressed.
  [SerializeField]
  private GameObject popup, settingContent;
  [SerializeField]
  private Transform content;
  [SerializeField]
  private Vector3 backgroundOffset;

  // Method which shows the pop-up overlay of the selected setting
  public void ShowSetting(Setting setting, Vector3 settingButtonPosition)
  {
    popup.transform.position = settingButtonPosition + backgroundOffset;
    
    Vector3 contentOffset = backgroundOffset + new Vector3(0.0f, 50.0f, 0.0f);
    settingContent = setting.content;
    GameObject popUpContent = Instantiate(settingContent) as GameObject;
    popUpContent.transform.SetParent(content);
    content.GetChild(0).position = settingButtonPosition + contentOffset;
    popup.SetActive(true);
  }

  // Method to close the setting-overlay pop-up
  public void HideSetting()
  {
    popup.SetActive(false);
    foreach (Transform child in content)
    {
      GameObject.Destroy(child.gameObject);
    }
  }

}
