using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{

  public TextMeshProUGUI title;
  // public TextMeshProUGUI dutch;
  // public TextMeshProUGUI english;
  // public TextMeshProUGUI german;

  public TextMeshProUGUI[] languages;
  public string[] dutch;
  public string[] english;
  public string[] german;

  public void SetLanguage(int index)
  {
    if(index == 0)
    {
        title.text = "Taal";
        int counter = 0;
        foreach(TextMeshProUGUI language in languages)
        {
          language.text = dutch[counter];
          counter++;
        }
    }
    else if(index == 1)
    {
        title.text = "Language";
        int counter = 0;
        foreach(TextMeshProUGUI language in languages)
        {
          language.text = english[counter];
          counter++;
        }
    }
    else if(index == 2)
    {
        title.text = "Sprache";
        int counter = 0;
        foreach(TextMeshProUGUI language in languages)
        {
          language.text = german[counter];
          counter++;
        }
    }
  }

}
