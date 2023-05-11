using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
  public static ResourceManager Instance;
  public List<ARItem> arItems = new List<ARItem>();
  public List<Subject> subjects = new List<Subject>();

  private void Awake()
  {
    if (Instance == null) { Instance = this; }

    arItems.AddRange(Resources.LoadAll<ARItem>("ARItems"));
    subjects.AddRange(Resources.LoadAll<Subject>("Subjects"));
  }
}
