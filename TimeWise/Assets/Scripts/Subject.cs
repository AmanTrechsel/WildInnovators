using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Subject", menuName = "Subject", order = 1)]
public class Subject : ScriptableObject
{
  public string name;
  public List<ARItem> items;
}