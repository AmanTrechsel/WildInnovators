using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARWarning : MonoBehaviour
{
  void Awake()
  {
    if (AppManager.Instance.warningAgreed) { AppManager.Instance.LoadScene("AR"); }
  }

  public void Agree()
  {
    AppManager.Instance.warningAgreed = true;
    AppManager.Instance.LoadScene("AR");
  }

  public void Disagree()
  {
    AppManager.Instance.warningAgreed = false;
    AppManager.Instance.LoadScene("Selection");
  }
}
