using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Subject", menuName = "ScriptableObjects/Subject", order = 1)]
public class Subject : ScriptableObject
{
  public uint id;
  public string name;
  public List<ARItem> items;
}