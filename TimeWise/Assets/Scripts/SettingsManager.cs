using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization.Settings;
using System.IO;

public class SettingsManager : MonoBehaviour
{

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

}
