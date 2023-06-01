using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization.Settings;
using System.IO;

public class SettingsManager : MonoBehaviour
{
  // Singleton
  public static SettingsManager Instance;

  private string saveFile = "time.wise";

  private void Awake()
  {
    if(Instance == null)
    {
      Instance = this;
    }
  }

  // Method for saving the settings on a local file
  public void SaveData()
  {
    SettingsData data = new SettingsData();
    data.permission = AppManager.Instance.permission;
    data.languageIndex = AppManager.Instance.languageIndex;
    
    string json = JsonUtility.ToJson(data);
    File.WriteAllText(Application.dataPath + "/" + saveFile, json);   // Path might need to be changed.
  }

  // Method for loading the settings from a local file
  public void LoadData()
  {
    string json = File.ReadAllText(Application.dataPath + "/" + saveFile);  // Path then also needs to be changed here.
    SettingsData data = JsonUtility.FromJson<SettingsData>(json);

    AppManager.Instance.permission = data.permission;
    AppManager.Instance.languageIndex = data.languageIndex;
  }

  // Probably want the settings to load from the homescreen so this probably needs to happen in AppManager.cs.
  void awake()
  {
    LoadData();
  }

  // Also .json isn't the most secure format, might want to change it to a binary file or something like that.



  // For the permission setting. The parameters are given through the seperate PermissionManager.cs script
  public void SwitchPermission(Button button, Sprite toggleOff, Sprite toggleOn)
  {
    if(button.image.sprite == toggleOn)
    {
      button.image.sprite = toggleOff;
      AppManager.Instance.permission = false;
    }
    else if(button.image.sprite == toggleOff)
    {
      button.image.sprite = toggleOn;
      AppManager.Instance.permission = true;
    }
  }

  // For the language setting. The parameter is given through the seperate LanguageManager.cs script
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

  // For the location setting
  public GameObject inputField;
  public string location = "TimeWise";

  public void ChangeLocation()
  {
    location = inputField.GetComponent<TMP_InputField>().text;
    Debug.Log(location);
    // With this location variable the path-variable in ARManager.cs needs to be changed
    // Plus it needs some checks and also needs to be set to a default value "TimeWise"
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
    foreach(Transform child in content)
    {
      GameObject.Destroy(child.gameObject);
    }
  }

}
