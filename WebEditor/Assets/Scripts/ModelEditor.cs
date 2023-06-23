using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System.Collections;

public class ModelEditor : MonoBehaviour
{
  // Singleton
  public static ModelEditor instance;

  // Reference to the model
  [HideInInspector]
  public GameObject model;
  // Bytes for the model
  [HideInInspector]
  public byte[] modelBytes;
  // List of uploaded textures
  public List<Texture2D> uploadedTextures = new List<Texture2D>();
  
  // Reference to the finish button animator
  [SerializeField]
  private Animator finishButtonAnimator;
  // Reference to the model editor
  [SerializeField]
  private GameObject modelEditor;
  // Reference to the encyclopedia editor
  [SerializeField]
  private GameObject encyclopediaEditor;
  // Reference to the material editor
  [SerializeField]
  private MaterialEditor materialEditor;
  // Reference to the material content
  [SerializeField]
  private Transform materialContent;
  // Prefab for the material button
  [SerializeField]
  private GameObject materialButtonPrefab;
  // List of dropdowns that use uploaded textures
  [SerializeField]
  private List<TMP_Dropdown> textureDropdowns;
  // Image used for the encyclopedia
  [SerializeField]
  private TMP_Dropdown encyclopediaImageDropdown;
  // Description used for the encyclopedia
  [SerializeField]
  private TMP_InputField encyclopediaDescription;
  // Reference to the name input field
  [SerializeField]
  private TMP_InputField inputName, inputNameEncyclopedia;
  // Reference to the position input fields
  [SerializeField]
  private TMP_InputField inputPositionX, inputPositionY, inputPositionZ;
  // Reference to the rotation input fields
  [SerializeField]
  private TMP_InputField inputRotationX, inputRotationY, inputRotationZ;
  // Reference to the scale input fields
  [SerializeField]
  private TMP_InputField inputScaleX, inputScaleY, inputScaleZ;
  // Reference to the finalize button
  [SerializeField]
  private CustomButton finalizeButton;

  // List of material buttons
  private List<MaterialButton> _materialButtons = new List<MaterialButton>();
  // Reference to the default shader
  private Shader defaultShader;
  // The id for the model that has just been uploaded
  private int uploadedId;

// Checks whether you are using WebGL and not from the editor
#if UNITY_WEBGL && !UNITY_EDITOR
  // WebGL
  [DllImport("__Internal")]
  private static extern void SetCookie(string cookie, int value);

  private void SetModelId(int value)
  {
    SetCookie("modelsUploaded", uploadedId);
  }
#else
  private void SetModelId(int value)
  {
    Debug.Log("Tried setting cookie to: " + value);
  }
#endif

  // Called when the script instance is being loaded
  private void Awake()
  {
    // Make sure there is only one instance of this class
    if (instance == null) { instance = this; }
    else { Destroy(gameObject); }

    // Get the default shader
    defaultShader = Shader.Find("Standard");
  }

  // Called every frame
  private void Update()
  {
    // Check if the model is not null
    if (model != null)
    {
      // Update the position, rotation and scale of the model
      model.transform.position = new Vector3(StringToFloat(inputPositionX.text), StringToFloat(inputPositionY.text), StringToFloat(inputPositionZ.text));
      model.transform.rotation = Quaternion.Euler(StringToFloat(inputRotationX.text), StringToFloat(inputRotationY.text), StringToFloat(inputRotationZ.text));
      model.transform.localScale = new Vector3(StringToFloat(inputScaleX.text), StringToFloat(inputScaleY.text), StringToFloat(inputScaleZ.text));
    }  
  }

  // Tries to parse a float from a string
  public static float StringToFloat(string input)
  {
    // Try to parse the input string to a float
    return (float.TryParse(input, out float result)) ? result : 0f;
  }

  // Updates the name from the Model Edit window
  public void ChangeName()
  {
    inputNameEncyclopedia.text = inputName.text;
  }

  // Updates the name from the Encyclopedia Edit window
  public void ChangeNameEncyclopedia()
  {
    inputName.text = inputNameEncyclopedia.text;
  }

  // Used when the user wants to finish editing the model
  public void Finalize()
  {
    // Check if the model is not null
    if (model == null)
    {
      // Show an error message
      Debug.LogError("No model selected");
      finishButtonAnimator.SetTrigger("Error");
      return;
    }

    // Initialize lists for the data
    List<Texture2D> images = new List<Texture2D>();
    List<Color> colors = new List<Color>();
    List<float> metallics = new List<float>();
    List<float> smoothnesses = new List<float>();

    // Get the name of the model
    string name = inputName.text;
    if (name == "") { name = "Unnamed"; }

    // Iterate through all the materials of the model
    foreach (MeshRenderer meshRenderer in model.GetComponentsInChildren<MeshRenderer>())
    {
      foreach (Material material in meshRenderer.materials)
      {
        // Add the data to the lists
        Texture2D texture = material.GetTexture("_MainTex") as Texture2D;
        if (texture == null) { texture = Texture2D.whiteTexture; }
        images.Add(texture);
        colors.Add(material.color);
        metallics.Add(material.GetFloat("_Metallic"));
        smoothnesses.Add(material.GetFloat("_Glossiness"));
      }
    }

    // Get the image for the encyclopedia
    Texture2D encyclopediaImage;
    if (encyclopediaImageDropdown.value == 0) { encyclopediaImage = Texture2D.whiteTexture; }
    else { encyclopediaImage = images[encyclopediaImageDropdown.value - 1]; }

    // Create a new serialized data object and save it to a json file
    SerializedData data = SerializedData.Create(name, model.transform.position, model.transform.rotation.eulerAngles, model.transform.localScale, images.ToArray(), colors.ToArray(), metallics.ToArray(), smoothnesses.ToArray(), encyclopediaImage, encyclopediaDescription.text);
    string json = JsonUtility.ToJson(data);

    // Send the json file to the database
    UploadToDatabase(json);
  }

  // Used when the user wants to add a model
  public void AddModel(GameObject addedModel, byte[] addedModelBytes)
  {
    // Remove the current model
    RemoveModel();

    // Set the model
    model = addedModel;

    // Set the model bytes
    modelBytes = addedModelBytes;

    // Remove all existing material buttons
    foreach (Transform child in materialContent)
    {
      Destroy(child.gameObject);
    }

    // Initialize a new list of material buttons
    _materialButtons = new List<MaterialButton>();

    // Iterate through all the materials of the model
    foreach (MeshRenderer meshRenderer in model.GetComponentsInChildren<MeshRenderer>())
    {
      foreach (Material material in meshRenderer.materials)
      {
        // Create a new material button and add it to the list
        material.shader = defaultShader;
        GameObject materialButton = Instantiate(materialButtonPrefab);
        MaterialButton materialButtonComponent = materialButton.GetComponent<MaterialButton>();
        materialButtonComponent.SetMaterial(material);
        _materialButtons.Add(materialButtonComponent);
        materialButton.transform.SetParent(materialContent);
      }
    }

    // Resize the material content
    materialContent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 50 * materialContent.childCount);
  }

  // Used when the user wants to remove the model
  public void RemoveModel()
  {
    // Check if the model is not null
    if (model != null)
    {
      // Destroy the model
      Destroy(model);
      model = null;
    }
  }

  // Used when the user wants to upload a texture
  public void AddUploadedImage(Texture2D texture)
  {
    // Iterate through all the dropdowns that use uploaded textures
    foreach (TMP_Dropdown dropdown in textureDropdowns)
    {
      // Add the texture to the dropdown
      dropdown.options.Add(new TMP_Dropdown.OptionData(texture.name, Sprite.Create(texture, new Rect(0,0,texture.width,texture.height), new Vector2(0.5f, 0.5f))));
    }

    // Add the texture to the list of uploaded textures
    uploadedTextures.Add(texture);
  }

  // Used when the user wants to start editing a material
  public void EditMaterial(Material material)
  {
    // Disable the model and encyclopedia editor and enable the material editor
    modelEditor.SetActive(false);
    encyclopediaEditor.SetActive(false);
    materialEditor.gameObject.SetActive(true);

    // Set the material of the material editor
    materialEditor.material = material;

    // Reset the inputs of the material editor
    materialEditor.ResetInputs();
  }

  // Used when the user wants to go back to the model editor
  public void BackToModelEditor()
  {
    // Update the thumbnails of the material buttons
    foreach (MaterialButton materialButton in _materialButtons)
    {
      materialButton.UpdateThumbnail();
    }

    // Disable the material and encyclopedia editor and enable the model editor
    modelEditor.SetActive(true);
    materialEditor.gameObject.SetActive(false);
    encyclopediaEditor.SetActive(false);
  }

  // Used when the user wants to start editing the encyclopedia
  public void EditEncyclopedia()
  {
    // Disable the model and material editor and enable the encyclopedia editor
    modelEditor.SetActive(false);
    materialEditor.gameObject.SetActive(false);
    encyclopediaEditor.SetActive(true);
  }

  // Uploads the model to the database
  public void UploadToDatabase(string json)
  {
    // Disable the finalize button so the user can't upload the model multiple times
    finalizeButton.interactable = false;

    // Create a new form
    WWWForm form = new WWWForm();

    // Add the json file to the form
    form.AddField("json", json);

    // Send the form to the database
    StartCoroutine(UploadToDatabaseCoroutine(form));
  }

  // Coroutine that uploads the model to the database
  private IEnumerator UploadToDatabaseCoroutine(WWWForm form)
  {
    // Send the form to the database
    UnityWebRequest www = UnityWebRequest.Post("https:://timewise.serverict.nl/upload_json.php", form);
    yield return www.SendWebRequest();

    // Check if there are any errors
    if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
    {
      // Show an error message
      Debug.LogError(www.error);

      // Set the upload id to -1
      uploadedId = -1;
    }
    else
    {
      // Show a success message
      Debug.Log("Upload complete!");

      // Get the upload id
      Int32.TryParse(www.downloadHandler.text, out uploadedId);
    }

    // Set cookie data to the uploaded code if it was successful
    if (uploadedId > -1) { SetModelId(uploadedId); }

    // Enable the finalize button again
    finalizeButton.interactable = true;

    // Remove the model
    RemoveModel();

    // Reset the inputs
    inputName.text = "";
    encyclopediaDescription.text = "";
    encyclopediaImageDropdown.value = 0;
    foreach (TMP_Dropdown dropdown in textureDropdowns)
    {
      dropdown.value = 0;
    }
    foreach (Transform child in materialContent)
    {
      Destroy(child.gameObject);
    }
    materialContent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
  }
}