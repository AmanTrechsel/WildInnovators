using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class Localization : MonoBehaviour
{
  // Languages: 0 = Dutch, 1 = English, 2 = German

  // Called when the script is loaded
  private void Awake()
  {
    // Set the language to first index (Dutch)
    ChangeLocale(0);
  }

  // Change the language
  public void ChangeLocale(int languageIdentifier)
  {
    // Set the language
    StartCoroutine(SetLanguage(languageIdentifier));
  }

  // Set the language
  IEnumerator SetLanguage(int index)
  {
    // Wait for the localization to be initialized
    yield return LocalizationSettings.InitializationOperation;
    
    // Set the language to the given index
    LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
  }
}