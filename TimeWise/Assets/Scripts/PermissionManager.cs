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

  // Called once at the start of the script
  private void Awake()
  {
    // Set the button's sprite to the correct one
    if(AppManager.Instance.permission)
    {
      button.image.sprite = toggleOn;
    }
    else
    {
      button.image.sprite = toggleOff;
    }
  }

  // Calls the SwitchPermission method in SettingsManager.cs.
  public void SwitchPerms()
  {
    SettingsManager.Instance.SwitchPermission(button, toggleOff, toggleOn);
  }
}
