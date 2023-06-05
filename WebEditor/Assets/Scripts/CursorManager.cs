using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
  public static CursorManager instance;

  [SerializeField]
  private List<Texture2D> cursorSprites;
  [SerializeField]
  private List<Vector2> cursorHotspots;

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
  }

  public void SetCursor(int cursorIndex)
  {
    Cursor.SetCursor(cursorSprites[cursorIndex], cursorHotspots[cursorIndex], CursorMode.Auto);
  }
}
