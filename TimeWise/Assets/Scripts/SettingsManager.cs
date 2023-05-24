using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization.Settings;

public class SettingsManager : MonoBehaviour
{
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
    }
    else if(button.image.sprite == toggleOff)
    {
      button.image.sprite = toggleOn;
    }
  }

  public void ChangeLanguage(int ID)
  {
    StartCoroutine(SetLanguage(ID));
  }

  IEnumerator SetLanguage(int index)
  {
    yield return LocalizationSettings.InitializationOperation;
    LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
  }

}
