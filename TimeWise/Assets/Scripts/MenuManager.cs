using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
  // Gets the index of the child within the parent object "MenuButtons".
  public void GetIndex()
  {
      int index = transform.GetSiblingIndex();
      AppManager.Instance.arSubject = ResourceManager.Instance.GetSubjectByID(index);
      Debug.Log(index);
  }

  // Switches to the ARScene, which will contain the subject selected at the function above here.
  public void GoToARScene()
  {
      SceneManager.LoadScene("AR");
      Debug.Log(AppManager.Instance.arSubject);
  }

  
  // Function for the settingsbutton
  public void GoToSettings()
  {
    SceneManager.LoadScene("Settings");
  }
}
