using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PermissionManager : MonoBehaviour
{
  // The button to switch the permission of
  [SerializeField]
  private Button button;
  // The sprite for the toggle off state
  [SerializeField]
  private Sprite toggleOff, toggleOn;

  // Calls the SwitchPermission method in SettingsManager.cs.
  public void SwitchPerms()
  {
    SettingsManager.Instance.SwitchPermission(button, toggleOff, toggleOn);
  }
}
