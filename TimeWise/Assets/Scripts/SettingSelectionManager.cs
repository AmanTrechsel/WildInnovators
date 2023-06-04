using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
  // The content rect transform
  [SerializeField]
  private RectTransform contentRect;
  // Layout group for the content
  [SerializeField]
  private VerticalLayoutGroup contentLayoutGroup;

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

    // Setup the content's height with the padding
    float contentHeight = contentLayoutGroup.padding.top + contentLayoutGroup.padding.bottom;

    // Update the content's height to be the sum of all the items' heights
    foreach (RectTransform child in settingContent)
    {
      // Add the height of the child along with the padding multiplied by its scale to the content's height
      contentHeight += (child.sizeDelta.y + contentLayoutGroup.spacing) * child.localScale.y;
    }

    // 130% multiplier to the content's height
    contentHeight *= 1.3f;

    // Set the content's height to contentHeight
    contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, contentHeight);
  }
}
