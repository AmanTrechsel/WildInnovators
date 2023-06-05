using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
  // Calls the ChangeLanguage method in SettingsManager.cs. This script is only used in the prefab.
  public void LanguageSwitch(int index)
  {
    SettingsManager.Instance.ChangeLanguage(index);
  }
}
