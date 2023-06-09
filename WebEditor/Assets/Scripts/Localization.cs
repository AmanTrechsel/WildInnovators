using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class Localization : MonoBehaviour
{
  private void Awake()
  {
    ChangeLocale(0);
  }

  public void ChangeLocale(int languageIdentifier)
  {
    StartCoroutine(SetLanguage(languageIdentifier));
  }

  IEnumerator SetLanguage(int index)
  {
    yield return LocalizationSettings.InitializationOperation;
    LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
  }
}