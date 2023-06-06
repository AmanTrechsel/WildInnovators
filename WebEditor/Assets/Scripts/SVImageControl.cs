using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SVImageControl : MonoBehaviour, IDragHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
  [SerializeField]
  private Image pickerImage, pickerImageOuter;
  private RawImage SVImage;
  public ColorPickerControl cpc;
  private RectTransform rectTransform, pickerTransform;

  private void Awake()
  {
    SVImage = GetComponent<RawImage>();
    rectTransform = GetComponent<RectTransform>();

    pickerTransform = pickerImage.GetComponent<RectTransform>();
    pickerTransform.localPosition = new Vector2(-(rectTransform.sizeDelta.x * 0.5f), -(rectTransform.sizeDelta.y * 0.5f));
  }

  public void SetPickerToColor(Color color)
  {
    float hue;
    float saturation;
    float value;
    Color.RGBToHSV(color, out hue, out saturation, out value);
    
    float sizeX = rectTransform.rect.width * 0.5f;
    float sizeY = rectTransform.rect.height * 0.5f;

    pickerTransform.localPosition = new Vector2(rectTransform.rect.width * saturation - sizeX, rectTransform.rect.height * value - sizeY);
  }

  void UpdateColor(PointerEventData eventData)
  {
    Vector3 pos = rectTransform.InverseTransformPoint(eventData.position);

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

    float x = pos.x + deltaX;
    float y = pos.y + deltaY;

    float xNorm = x / rectTransform.sizeDelta.x;
    float yNorm = y / rectTransform.sizeDelta.y;

    cpc.SetSV(xNorm, yNorm);

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

  public void OnDrag(PointerEventData eventData)
  {
    UpdateColor(eventData);
  }

  public void OnPointerClick(PointerEventData eventData)
  {
    UpdateColor(eventData);
  }
}
