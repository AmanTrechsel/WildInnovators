using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CustomSlider : Slider, IPointerEnterHandler, IPointerExitHandler
{
  // The text that displays the value of the slider
  public void SetValueText(TextMeshProUGUI valueText)
  {
    // Set the value text
    valueText.text = value.ToString("0.00");
  }

  // Called when the mouse is over the object
  public override void OnPointerEnter(PointerEventData eventData)
  {
    // Set the cursor to the selection cursor
    CursorManager.instance.SetCursor(1);

    // Call the base method
    base.OnPointerEnter(eventData);
  }

  // Called when the mouse exits the object
  public override void OnPointerExit(PointerEventData eventData)
  {
    // Set the cursor to the default cursor
    CursorManager.instance.SetCursor(0);

    // Call the base method
    base.OnPointerEnter(eventData);
  }
}
