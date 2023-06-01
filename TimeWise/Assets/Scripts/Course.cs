using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Course", menuName = "ScriptableObjects/Course", order = 1)]
public class Course : ScriptableObject
{
  // The course ID
  public uint id;
  // The course name
  public string name;
  // The course button graphic
  public Sprite buttonGraphic;
  // Subjects in the course
  public List<Subject> subjects;
}