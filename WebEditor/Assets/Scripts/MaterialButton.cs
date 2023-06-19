using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class MaterialButton : MonoBehaviour
{
  // Text field for the name of the material
  [SerializeField]
  private TextMeshProUGUI buttonText;
  // Image field for the thumbnail of the material
  [SerializeField]
  private Image previewImage;

  // Reference to the material
  private Material _material;

  // Set the material and update the thumbnail
  public void SetMaterial(Material material)
  {
    // Set the material
    _material = material;

    // Set the name of the material
    buttonText.text = material.name.Replace(" (Instance)", "");

    // Update the thumbnail
    UpdateThumbnail();
  }

  public void EditMaterial()
  {
    ModelEditor.instance.EditMaterial(_material);
  }

  // Update the thumbnail
  public void UpdateThumbnail()
  {
    // Get the thumbnail of the material
    Texture2D texture = _material.mainTexture as Texture2D;

    // Make sure the texture is not null
    if (texture == null) { texture = new Texture2D(1,1); }

    // Set the thumbnail and give it the color of the material
    previewImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    previewImage.color = _material.GetColor("_Color");
  }
}
