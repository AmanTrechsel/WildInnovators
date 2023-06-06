using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColorPickerControl : MonoBehaviour
{
  public float currentHue, currentSaturation, currentValue;
  [SerializeField]
  private RawImage hueImage, satValImage, outputImage;
  [SerializeField]
  private Slider hueSlider;
  [SerializeField]
  private Image pickerImage, pickerImageOuter;
  [SerializeField]
  private TMP_InputField hexInputField;
  [SerializeField]
  SVImageControl sv;
  private Texture2D hueTexture, svTexture, outputTexture;

  private void Start()
  {
    CreateHueImage();
    CreateSVImage();
    CreateOutputImage();
    UpdateOutputImage();
  }

  private void CreateHueImage()
  {
    hueTexture = new Texture2D(1, 16);
    hueTexture.wrapMode = TextureWrapMode.Clamp;
    hueTexture.name = "HueTexture";

    for(int i = 0; i < hueTexture.height; i++)
    {
      hueTexture.SetPixel(0, i, Color.HSVToRGB((float)i / hueTexture.height, 1f, 0.95f));
    }

    hueTexture.Apply();
    currentHue = 0;

    hueImage.texture = hueTexture;
  }

  private void CreateSVImage()
  {
    svTexture = new Texture2D(16, 16);
    svTexture.wrapMode = TextureWrapMode.Clamp;
    svTexture.name = "SatValTexture";

    for(int y = 0; y < svTexture.height; y++)
    {
      for(int x = 0; x < svTexture.width; x++)
      {
        svTexture.SetPixel(x, y, Color.HSVToRGB(currentHue, (float)x/svTexture.width, (float)y/svTexture.height));
      }
    }

    svTexture.Apply();
    currentSaturation = 0;
    currentValue = 1;

    satValImage.texture = svTexture;
  }

  private void CreateOutputImage()
  {
    outputTexture = new Texture2D(1, 16);
    outputTexture.wrapMode = TextureWrapMode.Clamp;
    outputTexture.name = "OutputTexture";

    Color currentColor = CurrentColor();

    for(int i = 0; i < outputTexture.height; i++)
    {
      outputTexture.SetPixel(0, i, currentColor);
    }

    outputTexture.Apply();

    outputImage.texture = outputTexture;
  }

  public Color CurrentColor()
  {
    return Color.HSVToRGB(currentHue, currentSaturation, currentValue);
  }

  private void UpdateOutputImage()
  {
    Color currentColor = CurrentColor();

    for(int i = 0; i < outputTexture.height; i++)
    {
      outputTexture.SetPixel(0, i, currentColor);
    }

    outputTexture.Apply();

    hexInputField.text = ColorUtility.ToHtmlStringRGB(currentColor);
  }

  public void SetSV(float s, float v)
  {
    currentSaturation = s;
    currentValue = v;

    sv.SetPickerToColor(CurrentColor());

    UpdateOutputImage();
  }

  public void ChangeSlider(float hue)
  {
    currentHue = hue;
    hueSlider.value = hue;
  }

  public void UpdatePicker(Color color)
  {
    pickerImage.color = color;
    pickerImageOuter.color = new Color(1.0f-pickerImage.color.r,1.0f-pickerImage.color.g,1.0f-pickerImage.color.b);
  }

  public void UpdateSVImage()
  {
    currentHue = hueSlider.value;

    for(int y = 0; y < svTexture.height; y++)
    {
      for(int x = 0; x < svTexture.width; x++)
      {
        svTexture.SetPixel(x, y, Color.HSVToRGB(currentHue, (float)x/svTexture.width, (float)y/svTexture.height));
      }
    }

    svTexture.Apply();

    UpdatePicker(CurrentColor());
    UpdateOutputImage();
  }

  public void OnTextInput()
  {
    if (hexInputField.text.Length < 6) { return; }

    Color newCol;

    if (ColorUtility.TryParseHtmlString("#" + hexInputField.text, out newCol))
      Color.RGBToHSV(newCol, out currentHue, out currentSaturation, out currentValue);

    hueSlider.value = currentHue;
    pickerImage.color = CurrentColor();
    pickerImageOuter.color = new Color(1.0f-pickerImage.color.r,1.0f-pickerImage.color.g,1.0f-pickerImage.color.b);

    UpdateOutputImage();
  }

  public void RandomizeColor()
  {
    currentHue = Random.Range(0f, 1f);
    currentSaturation = Random.Range(0f, 1f);
    currentValue = Random.Range(0f, 1f);

    hueSlider.value = currentHue;

    UpdateOutputImage();
  }
}
