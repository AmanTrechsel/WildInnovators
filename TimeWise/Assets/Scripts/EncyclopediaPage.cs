using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EncyclopediaPage", menuName = "ScriptableObjects/EncyclopediaPage", order = 1)]
public class EncyclopediaPage : ScriptableObject
{
  public uint id;
  public string displayName;
  public Sprite displayImage;
  [TextArea]
  public string description;
  public Subject subject;
}