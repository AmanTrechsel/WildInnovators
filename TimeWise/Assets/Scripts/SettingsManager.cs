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

  private void Awake()
  {
    if(Instance == null)
    {
      Instance = this;
    }
  }

  // For saving data in the cloud
  private bool permission = true;
  private int languageIndex;

  public void SaveData()
  {
    SettingsData data = new SettingsData();
    data.permission = permission;
    data.languageIndex = languageIndex;
    
    string json = JsonUtility.ToJson(data);
    File.WriteAllText(Application.dataPath+"/DataFile.json", json);   // Path might need to be changed, because this needs to be uploaded to the cloud.
  }

  public void LoadData()
  {
    string json = File.ReadAllText(Application.dataPath+"/DataFile.json");  // Path then also needs to be changed here.
    SettingsData data = JsonUtility.FromJson<SettingsData>(json);

    permission = data.permission;
    languageIndex = data.languageIndex;
  }

  // Probably want the settings to load from the homescreen so this probably needs to happen in AppManager.cs.
  void awake()
  {
    LoadData();
  }

  // Also .json isn't the most secure format, might want to change it to a binary file or something like that.



  // For the permission setting
  [SerializeField]
  private Button button;

  [SerializeField]
  private Sprite toggleOff;

  [SerializeField]
  private Sprite toggleOn;

  public void SwitchPermission()
  {
    if(button.image.sprite == toggleOn)
    {
      button.image.sprite = toggleOff;
      permission = false;
    }
    else if(button.image.sprite == toggleOff)
    {
      button.image.sprite = toggleOn;
      permission = true;
    }
  }

  // For the language setting
  public void ChangeLanguage(int ID)
  {
    StartCoroutine(SetLanguage(ID));
    languageIndex = ID;
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

}
