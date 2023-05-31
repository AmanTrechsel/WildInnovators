using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
  public void LanguageSwitch(int index)
  {
    SettingsManager.Instance.ChangeLanguage(index);
  }
}
