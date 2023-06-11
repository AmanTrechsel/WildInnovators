using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SVImageControl : MonoBehaviour, IDragHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
  // Reference to the picker images
  [SerializeField]
  private Image pickerImage, pickerImageOuter;
   // Reference to the color picker control
  [SerializeField]
  private ColorPickerControl cpc;

  // Reference to the image for the saturation and value
  private RawImage SVImage;
  // Reference to the rect transform
  private RectTransform rectTransform, pickerTransform;

  // Called when the script instance is being loaded
  private void Awake()
  {
    // Get the references
    SVImage = GetComponent<RawImage>();
    rectTransform = GetComponent<RectTransform>();

    // Set the picker image to the bottom left corner
    pickerTransform = pickerImage.GetComponent<RectTransform>();
    pickerTransform.localPosition = new Vector2(-(rectTransform.sizeDelta.x * 0.5f), -(rectTransform.sizeDelta.y * 0.5f));
  }

  // Repositions the picker image
  public void SetPickerToColor(Color color)
  {
    // Initialize the HSV values
    float hue;
    float saturation;
    float value;

    // Convert the current color to HSV
    Color.RGBToHSV(color, out hue, out saturation, out value);
    
    // Set the picker image to the correct position
    float sizeX = rectTransform.rect.width * 0.5f;
    float sizeY = rectTransform.rect.height * 0.5f;

    // Reposition the picker image
    pickerTransform.localPosition = new Vector2(rectTransform.rect.width * saturation - sizeX, rectTransform.rect.height * value - sizeY);
  }

  // Updates the color picker
  void UpdateColor(PointerEventData eventData)
  {
    // Get the position of the mouse
    Vector3 pos = rectTransform.InverseTransformPoint(eventData.position);

    // Clamp the position
    float deltaX = rectTransform.sizeDelta.x * 0.5f;
    float deltaY = rectTransform.sizeDelta.y * 0.5f;
    if (pos.x < -deltaX)
    {
      pos.x = -deltaX;
    }
    else if (pos.x > deltaX)
    {
      pos.x = deltaX;
    }

    if (pos.y < -deltaY)
    {
      pos.y = -deltaY;
    }
    else if (pos.y > deltaY)
    {
      pos.y = deltaY;
    }

    // Calculate the normalized values
    float x = pos.x + deltaX;
    float y = pos.y + deltaY;

    float xNorm = x / rectTransform.sizeDelta.x;
    float yNorm = y / rectTransform.sizeDelta.y;

    // Set the saturation and value
    cpc.SetSV(xNorm, yNorm);

    // Set the picker image to the correct position and color
    pickerTransform.localPosition = pos;
    pickerImage.color = cpc.CurrentColor();
    pickerImageOuter.color = new Color(1.0f-pickerImage.color.r,1.0f-pickerImage.color.g,1.0f-pickerImage.color.b);
  }

  // Called when the mouse button is pressed
  public void OnPointerDown(PointerEventData eventData)
  {
    // Hide the cursor
    CursorManager.instance.HideCursor();
  }

  // Called when the mouse button is released
  public void OnPointerUp(PointerEventData eventData)
  {
    // Show the cursor
    CursorManager.instance.ShowCursor();
  }

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

  // Called when the mouse is dragged
  public void OnDrag(PointerEventData eventData)
  {
    // Update the color picker
    UpdateColor(eventData);
  }

  // Called when the mouse is clicked
  public void OnPointerClick(PointerEventData eventData)
  {
    // Update the color picker
    UpdateColor(eventData);
  }
}
