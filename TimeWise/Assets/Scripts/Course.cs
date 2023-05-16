using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Course", menuName = "ScriptableObjects/Course", order = 1)]
public class Course : ScriptableObject
{
  public uint id;
  public string name;
  public Sprite buttonGraphic;
  public List<Subject> subjects;
}