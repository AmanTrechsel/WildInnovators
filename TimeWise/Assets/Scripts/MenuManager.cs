using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
  // Method to select the subject
  public void SetSubjectByIndex(int index)
  {
    AppManager.Instance.arSubject = ResourceManager.Instance.GetSubjectByID(index);
  }

  // Switches to the ARScene, which will contain the subject selected at the function above here.
  public void GoToARScene()
  {
    SceneManager.LoadScene("AR");
  }
}
