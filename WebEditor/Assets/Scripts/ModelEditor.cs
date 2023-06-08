using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ModelEditor : MonoBehaviour
{
  public static ModelEditor instance;

  [HideInInspector]
  public GameObject model;
  public List<Texture2D> uploadedTextures = new List<Texture2D>();

  [SerializeField]
  private GameObject modelEditor;
  [SerializeField]
  private MaterialEditor materialEditor;
  [SerializeField]
  private Transform materialContent;
  [SerializeField]
  private GameObject materialButtonPrefab;
  [SerializeField]
  private List<TMP_Dropdown> textureDropdowns;
  [SerializeField]
  private TMP_InputField inputPositionX, inputPositionY, inputPositionZ;
  [SerializeField]
  private TMP_InputField inputRotationX, inputRotationY, inputRotationZ;
  [SerializeField]
  private TMP_InputField inputScaleX, inputScaleY, inputScaleZ;

  private List<MaterialButton> _materialButtons = new List<MaterialButton>();

  private void Awake()
  {
    if (instance == null)
    {
      instance = this;
    }
    else
    {
      Destroy(gameObject);
    }
  }

  private void Update()
  {
    if (model != null)
    {
      model.transform.position = new Vector3(float.Parse(inputPositionX.text), float.Parse(inputPositionY.text), float.Parse(inputPositionZ.text));
      model.transform.rotation = Quaternion.Euler(float.Parse(inputRotationX.text), float.Parse(inputRotationY.text), float.Parse(inputRotationZ.text));
      model.transform.localScale = new Vector3(float.Parse(inputScaleX.text), float.Parse(inputScaleY.text), float.Parse(inputScaleZ.text));
    }  
  }

  public void AddModel(GameObject addedModel)
  {
    RemoveModel();
    model = addedModel;

    foreach (Transform child in materialContent)
    {
      Destroy(child.gameObject);
    }

    _materialButtons = new List<MaterialButton>();

    foreach (MeshRenderer meshRenderer in model.GetComponentsInChildren<MeshRenderer>())
    {
      foreach (Material material in meshRenderer.materials)
      {
        GameObject materialButton = Instantiate(materialButtonPrefab);
        MaterialButton materialButtonComponent = materialButton.GetComponent<MaterialButton>();
        materialButtonComponent.SetMaterial(material);
        _materialButtons.Add(materialButtonComponent);
        materialButton.transform.SetParent(materialContent);
      }
    }

    materialContent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 50 * materialContent.childCount);
  }

  public void RemoveModel()
  {
    if (model != null)
    {
      Destroy(model);
      model = null;
    }
  }

  public void AddUploadedImage(Texture2D texture)
  {
    foreach (TMP_Dropdown dropdown in textureDropdowns)
    {
      dropdown.options.Add(new TMP_Dropdown.OptionData(texture.name, Sprite.Create(texture, new Rect(0,0,texture.width,texture.height), new Vector2(0.5f, 0.5f))));
    }
    uploadedTextures.Add(texture);
  }

  public void EditMaterial(Material material)
  {
    modelEditor.SetActive(false);
    materialEditor.gameObject.SetActive(true);
    materialEditor.material = material;
    materialEditor.ResetInputs();
  }

  public void BackToModelEditor()
  {
    foreach (MaterialButton materialButton in _materialButtons)
    {
      materialButton.UpdateThumbnail();
    }
    modelEditor.SetActive(true);
    materialEditor.gameObject.SetActive(false);
  }
}
