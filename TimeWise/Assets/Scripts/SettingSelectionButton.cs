using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingSelectionButton : MonoBehaviour
{
  [SerializeField]
  private TextMeshProUGUI buttonName;
  [SerializeField]
  private Image buttonImage;

  private Setting _setting;

  public void SetSetting(Setting newSetting)
  {
    _setting = newSetting;
    buttonImage.sprite = _setting.buttonGraphic;
    buttonName.text = _setting.name;
  }
}
