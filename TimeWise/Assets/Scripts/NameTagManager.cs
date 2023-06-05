using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NameTagManager : MonoBehaviour
{
  // The text field for the name tag
  [SerializeField]
  private TextMeshProUGUI nameTag;
  // The canvas for the name tag
  [SerializeField]
  private Canvas canvas;

  // Setup the name tag
  public void SetupNametag(Camera cam, string text = "")
  {
    // Check if a camera was passed in, if so set it as the world camera
    if (cam != null) { canvas.worldCamera = cam; }
    // Set the name tag text
    nameTag.text = text;
  }
}
