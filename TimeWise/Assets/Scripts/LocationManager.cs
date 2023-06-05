using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationManager : MonoBehaviour
{
  [SerializeField]
  private GameObject inputField;
  [SerializeField]
  private string location = "Timewise";

  // Calls the ChangeLocation method in SettingsManager.cs.
  public void LocationChange()
  {
    SettingsManager.Instance.ChangeLocation(inputField, location);
  }
}
