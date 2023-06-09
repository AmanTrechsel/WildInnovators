using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class MaterialButton : MonoBehaviour
{
  [SerializeField]
  private TextMeshProUGUI buttonText;
  [SerializeField]
  private Image previewImage;

  private Material _material;

  public void SetMaterial(Material material)
  {
    _material = material;
    buttonText.text = material.name.Replace(" (Instance)", "");
    UpdateThumbnail();
  }

  public void EditMaterial()
  {
    ModelEditor.instance.EditMaterial(_material);
  }

  public void UpdateThumbnail()
  {
    Texture2D texture = _material.mainTexture as Texture2D;//AssetPreview.GetAssetPreview(_material);
    if (texture == null) { texture = new Texture2D(1,1); }
    previewImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    previewImage.color = _material.GetColor("_Color");
  }
}
