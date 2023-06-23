using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Dropdown))]
public class AchievementDropdownController : MonoBehaviour
{
  private Dropdown m_dropdown;
  private Dropdown Dropdown
  {
    get
    {
      if (m_dropdown == null)
      {
        // Gets the dropdown menu component if the variable is null
        m_dropdown = GetComponent<Dropdown>();
      }
      return m_dropdown;
    }
  }

  // Action to be triggered when the dropdown value changes
  public Action<AchievementID> onValueChanged;

  private void Start()
  {
    // Run the following classes on start
    UpdateOptions();
    Dropdown.onValueChanged.AddListener(HandleDropdownValueChanged);
  }

  [ContextMenu("UpdateOptions()")]
  public void UpdateOptions()
  {
    // Clear existing options
    Dropdown.options.Clear();

    // Get all values of the AchievementID enum
    var ids = Enum.GetValues(typeof(AchievementID));
    foreach (AchievementID id in ids)
    {
      // Add each enum value as an option in the dropdown
      Dropdown.options.Add(new Dropdown.OptionData(id.ToString()));
    }
    // Refresh the displayed value of the dropdown
    Dropdown.RefreshShownValue();
  }

  private void HandleDropdownValueChanged(int value)
  {
    if (onValueChanged != null)
    {
      // Trigger the onValueChanged action with the selected achievement ID
      onValueChanged((AchievementID)value);
    }
  }
}
