using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour
{
  // Singleton
  public static CursorManager instance;

  // Sprites for the cursor
  // 0: Default, 1: Select, 2: Input, 3: Resize Horizontal, 4: Eyedropper
  [SerializeField]
  private List<Sprite> cursorSprites;
  // Hotspots for the cursor
  [SerializeField]
  private List<Vector2> cursorHotspots;

  // Image component for the cursor
  private Image _image;
  // Index of the current cursor
  private int _cursorIndex;

  // Called when the script is loaded
  private void Awake()
  {
    // Singleton
    if (instance == null) { instance = this; }
    else { Destroy(gameObject); }

    // Get the image component
    _image = GetComponent<Image>();
  }

  // Called every frame
  private void Update()
  {
    // Set the cursor position
    transform.position = Input.mousePosition + (new Vector3(cursorHotspots[_cursorIndex].x, cursorHotspots[_cursorIndex].y, 0) * 0.02f);

    // Hide the cursor if it's outside the screen
    Cursor.visible = (Input.mousePosition.x < 0 || Input.mousePosition.x > Screen.width || Input.mousePosition.y < 0 || Input.mousePosition.y > Screen.height);
  }

  // Set the cursor to the given index
  public void SetCursor(int cursorIndex)
  {
    // Set the cursor index and sprite
    _cursorIndex = cursorIndex;
    _image.sprite = cursorSprites[_cursorIndex];
  }

  // Make the cursor visible
  public void ShowCursor()
  {
    _image.enabled = true;
  }

  // Make the cursor invisible
  public void HideCursor()
  {
    _image.enabled = false;
  }
}
