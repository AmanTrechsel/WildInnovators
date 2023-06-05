using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelEditor : MonoBehaviour
{
  public static ModelEditor instance;

  [HideInInspector]
  public GameObject model;

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

  public void RemoveModel()
  {
    if (model != null)
    {
      Destroy(model);
      model = null;
    }
  }
}
