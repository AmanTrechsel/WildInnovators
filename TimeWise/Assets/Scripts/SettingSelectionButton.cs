using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingSelectionButton : MonoBehaviour
{
  // The text field for the button name
  [SerializeField]
  private TextMeshProUGUI buttonName;
  // The image for the button
  [SerializeField]
  private Image buttonImage;

  // The setting for the button
  private Setting _setting;

  // Set the setting for the button
  public void SetSetting(Setting newSetting)
  {
    // Set the setting, button image and name
    _setting = newSetting;
    buttonImage.sprite = _setting.buttonGraphic;
    buttonName.text = _setting.name;
  }

  // Show the setting
  public void ShowSetting()
  {
    // Call the ShowSetting method in SettingsManager.cs
    SettingsManager.Instance.ShowSetting(_setting, transform.position);
  }
}
