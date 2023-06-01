using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermissionManager : MonoBehaviour
{
  public void PermissionSwitch()
  {
    SettingsManager.Instance.SwitchPermission();
  }
}
