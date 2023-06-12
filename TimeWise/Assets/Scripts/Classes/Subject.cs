using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Subject", menuName = "ScriptableObjects/Subject", order = 1)]
public class Subject : ScriptableObject
{
  // The id of the subject
  public uint id;
  // The name of the subject
  public string name;
  // The graphic for the button
  public Sprite buttonGraphic;
  // The content info for the subject
  [TextArea]
  public string infoContent;
  // All AR items contained in the subject
  public List<ARItem> items;
}