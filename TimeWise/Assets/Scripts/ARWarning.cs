using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARWarning : MonoBehaviour
{
  // Awake is called when the script instance is being loaded
  void Awake()
  {
    // Check if the user has agreed to the warning and load the AR scene if they have
    if (AppManager.Instance.warningAgreed) { AppManager.Instance.LoadScene("AR"); }
  }

  // Agree to the warning and load the AR scene
  public void Agree()
  {
    // Set the warning agreed to true so the user will not see the warning again
    AppManager.Instance.warningAgreed = true;
    // Load the AR scene
    AppManager.Instance.LoadScene("AR");
  }

  // Disagree to the warning and go back to the selection scene
  public void Disagree()
  {
    // Set the warning agreed to false so the user will see the warning again
    AppManager.Instance.warningAgreed = false;
    // Load the selection scene
    AppManager.Instance.LoadScene("Selection");
  }
}
