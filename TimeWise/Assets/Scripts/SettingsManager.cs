using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{

  private TextMeshProUGUI title;

  void Start()
  {
    title = gameObject.GetComponent<TextMeshProUGUI>();
  }

  public void SetLanguage(int index)
  {
    if(index == 0)
    {
        title.text = "Taal";
    }
    else if(index == 1)
    {
        title.text = "Language";
    }
    else if(index == 2)
    {
        title.text = "Sprache";
        Debug.Log("duits");
    }
  }

}
