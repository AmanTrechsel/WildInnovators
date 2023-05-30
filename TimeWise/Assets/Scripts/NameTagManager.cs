using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NameTagManager : MonoBehaviour
{
  [SerializeField]
  private TextMeshProUGUI nameTag;
  [SerializeField]
  private Canvas canvas;

  public void SetupNametag(Camera cam, string text = "")
  {
    if (cam != null) { canvas.worldCamera = cam; }
    nameTag.text = text;
  }
}
