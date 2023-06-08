using UnityEngine.EventSystems;
using UnityEngine;

public class Clickable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
  // Called when the mouse is over the object
  public void OnPointerEnter(PointerEventData eventData)
  {
    // Set the cursor to the selection cursor
    CursorManager.instance.SetCursor(1);
  }

  // Called when the mouse exits the object
  public void OnPointerExit(PointerEventData eventData)
  {
    // Set the cursor to the default cursor
    CursorManager.instance.SetCursor(0);
  }
}
