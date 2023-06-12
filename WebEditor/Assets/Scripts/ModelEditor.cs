using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using MySql.Data.MySqlClient;
using System.Runtime.InteropServices;

public class ModelEditor : MonoBehaviour
{
  // Singleton
  public static ModelEditor instance;

  // Import the UploadJson function from the JavaScript file
  [DllImport("__Internal")]
  private static extern void UploadJson(string json);

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

  // List of material buttons
  private List<MaterialButton> _materialButtons = new List<MaterialButton>();
  // Reference to the default shader
  private Shader defaultShader;

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
      model.transform.position = new Vector3(float.Parse(inputPositionX.text), float.Parse(inputPositionY.text), float.Parse(inputPositionZ.text));
      model.transform.rotation = Quaternion.Euler(float.Parse(inputRotationX.text), float.Parse(inputRotationY.text), float.Parse(inputRotationZ.text));
      model.transform.localScale = new Vector3(float.Parse(inputScaleX.text), float.Parse(inputScaleY.text), float.Parse(inputScaleZ.text));
    }  
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

    // Write the json to a file
    File.WriteAllText(Application.dataPath + "/Model.json", json);

    // Send the json file to the database
    UploadToDatabase(json);

    // Send the json file to the JavaScript file
    UploadJson(json);

    // (Debug) Read the json from the file and reconstruct the serialized data object
    SerializedData.Reconstruct(JsonUtility.FromJson(json, typeof(SerializedData)) as SerializedData);
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
    // Create a new connection to the database
    MySqlConnection connection = new MySqlConnection("connection");

    // Open the connection
    connection.Open();

    // Create a new command
    MySqlCommand writeCommand = new MySqlCommand("INSERT INTO my_table (jasonFile) VALUES ();", connection);

    // Execute the command
    writeCommand.ExecuteNonQuery();

    // Create a new command
    MySqlCommand readCommand = new MySqlCommand("SELECT LAST_INSERT_ID();", connection);

    // Execute the command
    MySqlDataReader reader = readCommand.ExecuteReader();

    // Initialize the id
    int id = 0;

    // Read the result
    while (reader.Read())
    {
      // Get the id
      id = reader.GetInt32(0);
    }

    // Close the connection
    connection.Close();
  }
}
