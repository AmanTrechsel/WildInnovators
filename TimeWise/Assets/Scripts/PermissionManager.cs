using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PermissionManager : MonoBehaviour
{
  [SerializeField]
  private Button button;
  [SerializeField]
  private Sprite toggleOff, toggleOn;

  // Calls the SwitchPermission method in SettingsManager.cs. This script is only used in the prefab.
  public void SwitchPerms()
  {
    SettingsManager.Instance.SwitchPermission(button, toggleOff, toggleOn);
  }
}
