using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EncyclopediaPage", menuName = "ScriptableObjects/EncyclopediaPage", order = 1)]
public class EncyclopediaPage : ScriptableObject
{
  // The encyclopedia page ID
  public uint id;
  // The encyclopedia page display name
  public string displayName;
  // The encyclopedia page display image
  public Sprite displayImage;
  // The encyclopedia page description
  [TextArea]
  public string description;
  // The encyclopedia page subject
  public Subject subject;
}