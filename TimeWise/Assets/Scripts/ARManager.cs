using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARManager : MonoBehaviour
{
  public static ARManager Instance;
  public Camera arCamera;

  private void Awake()
  {
    if (Instance == null) { Instance = this; }
  }

  public void TakePicture()
  {
    // Initialize and render
    Texture2D photo = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
    RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
    arCamera.targetTexture = renderTexture;
    arCamera.Render();
    RenderTexture.active = renderTexture;
    
    // Read pixels
    photo.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
    
    // Clean up
    arCamera.targetTexture = null;
    RenderTexture.active = null;
    DestroyImmediate(renderTexture);

    // Make screenshot
    byte[] bytes = photo.EncodeToPNG();
    File.WriteAllBytes("photo.png", bytes);
  }

  public void BackToSelection()
  {
    AppManager.Instance.LoadScene("Selection");
  }
}
