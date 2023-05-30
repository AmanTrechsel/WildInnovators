using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Setting", menuName = "ScriptableObjects/Setting", order = 1)]
public class Setting : ScriptableObject
{
  public uint id;
  public string name;
  public Sprite buttonGraphic;
  public GameObject content;
}
