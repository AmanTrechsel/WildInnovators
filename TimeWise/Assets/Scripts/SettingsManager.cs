using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{

  // For the language setting
  // Defining variables
  [SerializeField]
  private TextMeshProUGUI title;

  [SerializeField]
  private TextMeshProUGUI[] languages;

  [SerializeField]
  private string[] dutch;

  [SerializeField]
  private string[] english;

  [SerializeField]
  private string[] german;

  // Switches the language dependent on the index, using terms in the Unity inspector
  public void SetLanguage(int index)
  {
    int counter = 0;
    switch(index)
    {
      case 0:
        title.text = "Taal";
        foreach(TextMeshProUGUI language in languages)
        {
          language.text = dutch[counter];
          counter++;
        }
        counter = 0;
        break;
      case 1:
        title.text = "Language";
        foreach(TextMeshProUGUI language in languages)
        {
          language.text = english[counter];
          counter++;
        }
        counter = 0;
        break;
      case 2:
        title.text = "Sprache";
        foreach(TextMeshProUGUI language in languages)
        {
          language.text = german[counter];
          counter++;
        }
        counter = 0;
        break;
    }
  }

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

}
