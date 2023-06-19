using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColorPickerControl : MonoBehaviour
{
  // Reference to the material editor
  [SerializeField]
  private MaterialEditor materialEditor;
  // Reference to the color picker images
  [SerializeField]
  private RawImage hueImage, satValImage, outputImage;
  // Reference to the color picker slider
  [SerializeField]
  private Slider hueSlider;
  // Reference to the color picker cursor
  [SerializeField]
  private Image pickerImage, pickerImageOuter;
  // Reference to the hex input field
  [SerializeField]
  private TMP_InputField hexInputField;
  // Reference to the saturation value control
  [SerializeField]
  private SVImageControl sv;

  // Textures used for the color picker
  private Texture2D hueTexture, svTexture, outputTexture;
  // Current color values
  private float currentHue, currentSaturation, currentValue;

  // Called when the script instance is being loaded
  private void Start()
  {
    // Create the color picker
    CreateHueImage();
    CreateSVImage();
    CreateOutputImage();
    UpdateOutputImage();
  }

  // Creates the hue image
  private void CreateHueImage()
  {
    // Create a texture for the hue image
    hueTexture = new Texture2D(1, 16);
    hueTexture.wrapMode = TextureWrapMode.Clamp;
    hueTexture.name = "HueTexture";

    // Loop through the height of the texture
    for(int i = 0; i < hueTexture.height; i++)
    {
      // Set the pixel to the color of the hue at the current height
      hueTexture.SetPixel(0, i, Color.HSVToRGB((float)i / hueTexture.height, 1f, 0.95f));
    }

    // Apply the texture
    hueTexture.Apply();

    // Set the current hue to 0
    currentHue = 0;

    // Set the hue image to the texture
    hueImage.texture = hueTexture;
  }

  // Creates the saturation value image
  private void CreateSVImage()
  {
    // Create a texture for the saturation value image
    svTexture = new Texture2D(16, 16);
    svTexture.wrapMode = TextureWrapMode.Clamp;
    svTexture.name = "SatValTexture";

    // Loop through the height of the texture
    for(int y = 0; y < svTexture.height; y++)
    {
      for(int x = 0; x < svTexture.width; x++)
      {
        // Set the pixel to the color of the saturation and value at the current width and height
        svTexture.SetPixel(x, y, Color.HSVToRGB(currentHue, (float)x/svTexture.width, (float)y/svTexture.height));
      }
    }

    // Apply the texture
    svTexture.Apply();

    // Set the current saturation and value to 0 and 1 respectively
    currentSaturation = 0;
    currentValue = 1;

    // Set the saturation value image to the texture
    satValImage.texture = svTexture;
  }

  // Creates the output image
  private void CreateOutputImage()
  {
    // Create a texture for the output image
    outputTexture = new Texture2D(1, 16);
    outputTexture.wrapMode = TextureWrapMode.Clamp;
    outputTexture.name = "OutputTexture";

    // Set the current color to the color of the output texture
    Color currentColor = CurrentColor();

    // Loop through the height of the texture
    for(int i = 0; i < outputTexture.height; i++)
    {
      // Set the pixel to the current color at the current height
      outputTexture.SetPixel(0, i, currentColor);
    }

    // Apply the texture
    outputTexture.Apply();

    // Set the output image to the texture
    outputImage.texture = outputTexture;
  }

  // Sets the color of the color picker
  public void SetColor(Color newColor)
  {
    // Initialize the hue, saturation, and value floats
    float hue;
    float saturation;
    float value;

    // Convert the color to HSV
    Color.RGBToHSV(newColor, out hue, out saturation, out value);

    // Set the current hue, saturation, and value to the converted values
    currentHue = hue;
    currentSaturation = saturation;
    currentValue = value;

    // Update the output image
    UpdateOutputImage();
  }

  // Returns the current color of the color picker
  public Color CurrentColor()
  {
    return Color.HSVToRGB(currentHue, currentSaturation, currentValue);
  }

  // Updates the output image
  private void UpdateOutputImage()
  {
    // Set the current color to the color of the output texture
    Color currentColor = CurrentColor();

    // If the output texture is null, create it
    if (outputTexture == null) { CreateOutputImage(); }
    
    // Loop through the height of the texture
    for(int i = 0; i < outputTexture.height; i++)
    {
      // Set the pixel to the current color at the current height
      outputTexture.SetPixel(0, i, currentColor);
    }

    // Apply the texture
    outputTexture.Apply();

    // Set the hex input field to the current color
    hexInputField.text = ColorUtility.ToHtmlStringRGB(currentColor);

    // Set the color of the material editor to the current color
    materialEditor.SetColor(currentColor);
  }

  // Updates the saturation and value
  public void SetSV(float s, float v)
  {
    // Set the current saturation and value to the given values
    currentSaturation = s;
    currentValue = v;

    // Update the position of the picker
    sv.SetPickerToColor(CurrentColor());

    // Update the output image
    UpdateOutputImage();
  }

  // Updates the hue
  public void ChangeSlider(float hue)
  {
    // Set the current hue to the given hue
    currentHue = hue;
    hueSlider.value = hue;
  }

  // Updates the color of the color picker
  public void UpdatePicker(Color color)
  {
    // Set the color of the picker image to the given color
    pickerImage.color = color;

    // Set the color of the outer picker image to the inverse of the given color
    pickerImageOuter.color = new Color(1.0f-pickerImage.color.r,1.0f-pickerImage.color.g,1.0f-pickerImage.color.b);
  }

  // Updates the saturation and value image
  public void UpdateSVImage()
  {
    // Get the current hue from the hue slider
    currentHue = hueSlider.value;

    // Loop through the height of the texture
    for(int y = 0; y < svTexture.height; y++)
    {
      for(int x = 0; x < svTexture.width; x++)
      {
        // Set the pixel to the color of the saturation and value at the current width and height
        svTexture.SetPixel(x, y, Color.HSVToRGB(currentHue, (float)x/svTexture.width, (float)y/svTexture.height));
      }
    }

    // Apply the texture
    svTexture.Apply();

    // Update the position of the picker and the output image
    UpdatePicker(CurrentColor());
    UpdateOutputImage();
  }

  // Called when the hex input field is changed
  public void OnTextInput()
  {
    // Only continue if the hex input field is 6 characters long
    if (hexInputField.text.Length < 6) { return; }

    // Initialize the new color
    Color newCol;

    // If the hex input field is a valid color, set the new color to the given color
    if (ColorUtility.TryParseHtmlString("#" + hexInputField.text, out newCol)) { Color.RGBToHSV(newCol, out currentHue, out currentSaturation, out currentValue); }

    // Update colors and output image
    hueSlider.value = currentHue;
    pickerImage.color = CurrentColor();
    pickerImageOuter.color = new Color(1.0f-pickerImage.color.r,1.0f-pickerImage.color.g,1.0f-pickerImage.color.b);
    UpdateOutputImage();
  }

  // Randomizes the color
  public void RandomizeColor()
  {
    // Set the current hue, saturation, and value to random values
    currentHue = Random.Range(0f, 1f);
    currentSaturation = Random.Range(0f, 1f);
    currentValue = Random.Range(0f, 1f);

    // Update the hue slider
    hueSlider.value = currentHue;

    // Update the output image
    UpdateOutputImage();
  }
}
