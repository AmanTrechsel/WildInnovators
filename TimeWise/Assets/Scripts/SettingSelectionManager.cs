using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingSelectionManager : MonoBehaviour
{
  // Singleton
  public static SettingSelectionManager Instance;

  [SerializeField]
  private GameObject settingSelectionButtonPrefab;
  [SerializeField]
  private Transform settingContent;

  // Called once at the start of the app
  private void Awake()
  {
    // Assign this instance as the singleton
    if (Instance == null) { Instance = this; }

    // Iterate through all courses found in ResourceManager
    foreach (Setting setting in ResourceManager.Instance.settings)
    {
      // Create a new course button based on the given prefab
      GameObject createdSettingButton = Instantiate(settingSelectionButtonPrefab) as GameObject;
      // Set the current course to the button
      createdSettingButton.GetComponent<SettingSelectionButton>().SetSetting(setting);
      // Add the created course button to the content list
      createdSettingButton.transform.SetParent(settingContent);
    }
  }
}
