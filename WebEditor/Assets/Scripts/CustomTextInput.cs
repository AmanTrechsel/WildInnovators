using TMPro;
using UnityEngine.EventSystems;

public class CustomTextInput : TMP_InputField, IPointerEnterHandler, IPointerExitHandler
{
  // Called when the mouse is over the object
  public override void OnPointerEnter(PointerEventData eventData)
  {
    // Set the cursor to the input cursor
    CursorManager.instance.SetCursor(2);

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
