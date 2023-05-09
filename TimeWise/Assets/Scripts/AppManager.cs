using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour
{
  public static AppManager Instance;
  public GameObject arDisplayObject;

  private void Awake()
  {
    if (Instance == null) { Instance = this; }
    DontDestroyOnLoad(gameObject);
  }
}
