using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourseSelectionManager : MonoBehaviour
{
  // Singleton
  public static CourseSelectionManager Instance;

  // The course selection button prefab
  [SerializeField]
  private GameObject courseSelectionButtonPrefab;
  // The course content transform
  [SerializeField]
  private Transform courseContent;

  // Called once at the start of the app
  private void Awake()
  {
    // Assign this instance as the singleton
    if (Instance == null) { Instance = this; }
    else if (Instance != this) { Destroy(gameObject); }

    // Iterate through all courses found in ResourceManager
    foreach (Course course in ResourceManager.Instance.courses)
    {
      // Create a new course button based on the given prefab
      GameObject createdCourseButton = Instantiate(courseSelectionButtonPrefab) as GameObject;
      // Set the current course to the button
      createdCourseButton.GetComponent<CourseSelectionButton>().SetCourse(course);
      // Add the created course button to the content list
      createdCourseButton.transform.SetParent(courseContent);
    }
  }
}
