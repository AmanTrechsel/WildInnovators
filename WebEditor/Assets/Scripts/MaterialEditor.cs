using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialEditor : MonoBehaviour
{
  // Reference to the material
  [HideInInspector]
  public Material material;

  // Reference to material property fields
  [SerializeField]
  private CustomSlider _opacitySlider, _metallicSlider, _smoothnessSlider;
  [SerializeField]
  private CustomDropdown _textureDropdown;
  [SerializeField]
  private ColorPickerControl _colorPicker;

  // Reset the material properties
  public void ResetInputs()
  {
    // Make the material opaque
    // From https://answers.unity.com/questions/1004666/change-material-rendering-mode-in-runtime.html
    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
    material.DisableKeyword("_ALPHATEST_ON");
    material.DisableKeyword("_ALPHABLEND_ON");
    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");

    // Set the material to be opaque or transparent based on the alpha value
    if (material.color.a < 1) { SetTransparent(); }
    else { SetOpaque(); }

    // Reset the material properties
    _opacitySlider.value = material.color.a;
    _metallicSlider.value = material.GetFloat("_Metallic");
    _smoothnessSlider.value = material.GetFloat("_Glossiness");
    _colorPicker.SetColor(material.color);
    _colorPicker.UpdatePicker(material.color);

    // Set the texture dropdown to the correct value
    foreach (Texture2D texture in ModelEditor.instance.uploadedTextures)
    {
      if (texture == material.mainTexture)
      {
        _textureDropdown.value = ModelEditor.instance.uploadedTextures.IndexOf(texture) + 1;
        return;
      }
    }
    _textureDropdown.value = 0;
  }

  // Set the material's texture
  public void SetTexture(int textureIndex)
  {
    // Check if the texture index is 0 (no texture)
    if (textureIndex == 0)
    {
      // Set the texture to null
      material.SetTexture("_MainTex", null);
      return;
    }

    // Set the texture to the selected texture
    material.SetTexture("_MainTex", ModelEditor.instance.uploadedTextures[textureIndex-1]);
  }
  
  // Set the material's color
  public void SetColor(Color color)
  {
    material.color = color;
  }

  // Set the material's opacity
  public void SetOpacity(float opacity)
  {
    // Set the material to be opaque or transparent based on the alpha value
    if (material.color.a == 1 && opacity < 1) { SetTransparent(); }
    else if (material.color.a < 1 && opacity == 1) { SetOpaque(); }

    // Set the opacity of the material
    material.color = new Color(material.color.r, material.color.g, material.color.b, opacity);
  }

  // Set the material's metallic
  public void SetMetallic(float metallic)
  {
    material.SetFloat("_Metallic", metallic);
  }

  // Set the material's smoothness
  public void SetSmoothness(float smoothness)
  {
    material.SetFloat("_Glossiness", smoothness);
  }

  // From https://answers.unity.com/questions/1004666/change-material-rendering-mode-in-runtime.html
  private void SetTransparent()
  {
    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
    material.SetInt("_ZWrite", 0);
    material.renderQueue = 3000;
  }

  private void SetOpaque()
  {
    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
    material.SetInt("_ZWrite", 1);
    material.renderQueue = -1;
  }
}
