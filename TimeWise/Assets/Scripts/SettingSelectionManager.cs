using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingSelectionManager : MonoBehaviour
{
  // Singleton
  public static SettingSelectionManager Instance;

  // The prefab for the setting selection button
  [SerializeField]
  private GameObject settingSelectionButtonPrefab;
  // Transform for the setting content
  [SerializeField]
  private Transform settingContent;

  // Called once at the start of the app
  private void Awake()
  {
    // Assign this instance as the singleton
    if (Instance == null) { Instance = this; }

    // Iterate through all settings found in ResourceManager
    foreach (Setting setting in ResourceManager.Instance.settings)
    {
      // Create a new setting button based on the given prefab
      GameObject createdSettingButton = Instantiate(settingSelectionButtonPrefab) as GameObject;
      // Set the current setting to the button
      createdSettingButton.GetComponent<SettingSelectionButton>().SetSetting(setting);
      // Add the created setting button to the content list
      createdSettingButton.transform.SetParent(settingContent);
    }
  }
}
