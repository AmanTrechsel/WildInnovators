using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour
{
  public static CursorManager instance;

  [SerializeField]
  private List<Sprite> cursorSprites;
  [SerializeField]
  private List<Vector2> cursorHotspots;

  private Image _image;
  private int _cursorIndex;

  private void Awake()
  {
    if (instance == null)
    {
      instance = this;
    }
    else
    {
      Destroy(gameObject);
    }

    _image = GetComponent<Image>();
  }

  private void Update()
  {
    transform.position = Input.mousePosition + (new Vector3(cursorHotspots[_cursorIndex].x, cursorHotspots[_cursorIndex].y, 0) * 0.02f);

    Cursor.visible = (Input.mousePosition.x < 0 || Input.mousePosition.x > Screen.width || Input.mousePosition.y < 0 || Input.mousePosition.y > Screen.height);
  }

  public void SetCursor(int cursorIndex)
  {
    _cursorIndex = cursorIndex;
    _image.sprite = cursorSprites[_cursorIndex];
  }

  public void ShowCursor()
  {
    _image.enabled = true;
  }

  public void HideCursor()
  {
    _image.enabled = false;
  }
}
