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
  private TMP_InputField inputPositionX, inputPositionY, inputPositionZ;
  [SerializeField]
  private TMP_InputField inputRotationX, inputRotationY, inputRotationZ;
  [SerializeField]
  private TMP_InputField inputScaleX, inputScaleY, inputScaleZ;

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

  public void RemoveModel()
  {
    if (model != null)
    {
      Destroy(model);
      model = null;
    }
  }
}
