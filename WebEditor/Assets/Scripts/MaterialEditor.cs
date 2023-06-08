using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialEditor : MonoBehaviour
{
  [HideInInspector]
  public Material material;

  [SerializeField]
  private CustomSlider _opacitySlider, _metallicSlider, _smoothnessSlider;
  [SerializeField]
  private CustomDropdown _textureDropdown;
  [SerializeField]
  private ColorPickerControl _colorPicker;

  public void ResetInputs()
  {
    // From https://answers.unity.com/questions/1004666/change-material-rendering-mode-in-runtime.html
    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
    material.DisableKeyword("_ALPHATEST_ON");
    material.DisableKeyword("_ALPHABLEND_ON");
    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
    if (material.color.a < 1)
    {
      SetTransparent();
    }
    else 
    {
      SetOpaque();
    }

    _opacitySlider.value = material.color.a;
    _metallicSlider.value = material.GetFloat("_Metallic");
    _smoothnessSlider.value = material.GetFloat("_Glossiness");
    _colorPicker.SetColor(material.color);
    _colorPicker.UpdatePicker(material.color);

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

  public void SetTexture(int textureIndex)
  {
    if (textureIndex == 0)
    {
      material.mainTexture = null;
      return;
    }
    material.mainTexture = ModelEditor.instance.uploadedTextures[textureIndex-1];
  }
  
  public void SetColor(Color color)
  {
    material.color = color;
  }

  public void SetOpacity(float opacity)
  {
    if (material.color.a == 1 && opacity < 1)
    {
      SetTransparent();
    }
    else if (material.color.a < 1 && opacity == 1)
    {
      SetOpaque();
    }
    material.color = new Color(material.color.r, material.color.g, material.color.b, opacity);
  }

  public void SetMetallic(float metallic)
  {
    material.SetFloat("_Metallic", metallic);
  }

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
