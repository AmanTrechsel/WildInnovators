using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class NumberInputModify : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
  // Reference to the input field that will be modified
  [SerializeField]
  private TMP_InputField inputField;

  // Set this to true when you want to start dragging
  private bool isPressed = false;
  // Store the position of the mouse when dragging starts
  private Vector2 previousMousePosition;

  // Called when the mouse button is released
  public void OnPointerUp(PointerEventData eventData)
  {
    // Stop dragging
    isPressed = false;

    CursorManager.instance.SetCursor(0);
  }

  // Called when the mouse button is pressed
  void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
  {
    CursorManager.instance.SetCursor(2);
    // Start dragging
    isPressed = true;
    // Store the position of the mouse
    previousMousePosition = eventData.position;
  }

  // Update is called once per frame
  void Update()
  {
    // Check if the mouse button is pressed
    if (isPressed)
    {
      // Get the current mouse position and calculate the delta position
      Vector2 currentMousePosition = Input.mousePosition;
      Vector2 mouseDelta = (currentMousePosition - previousMousePosition) * 0.0001f;

      // Get the current value of the input field
      float value = float.Parse(inputField.text);

      // Modify the input field value
      value = value + mouseDelta.x;

      // Set the input field value
      inputField.text = value.ToString();
    }

    if (isPressed && Input.GetMouseButton(0))
    {
      CursorManager.instance.SetCursor(3);
    }
    else
    {
      CursorManager.instance.SetCursor(0);
    }
  }
}
