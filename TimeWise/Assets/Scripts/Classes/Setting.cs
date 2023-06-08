using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Setting", menuName = "ScriptableObjects/Setting", order = 1)]
public class Setting : ScriptableObject
{
  // The id of the setting
  public uint id;
  // The name of the setting
  public string name;
  // The graphic for the button
  public Sprite buttonGraphic;
  // The content for the setting
  public GameObject content;
}
