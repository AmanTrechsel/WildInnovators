using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
  [SerializeField]
  private RectTransform contentRect;
  [SerializeField]
  private VerticalLayoutGroup contentLayoutGroup;
  // Message shown when no courses were added
  [SerializeField]
  private GameObject noneFoundMessage;

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

    // Setup the content's height with the padding
    float contentHeight = contentLayoutGroup.padding.top + contentLayoutGroup.padding.bottom;

    // Update the content's height to be the sum of all the items' heights
    foreach (RectTransform child in courseContent)
    {
      // Add the height of the child along with the padding multiplied by its scale to the content's height
      contentHeight += (child.sizeDelta.y + contentLayoutGroup.spacing) * child.localScale.y;
    }

    // 130% multiplier to the content's height
    contentHeight *= 1.3f;

    // Set the content's height to contentHeight
    contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, contentHeight);

    // Check whether any courses were added
    if (courseContent.childCount == 0)
    {
      noneFoundMessage.SetActive(true);
    }
  }

  // Opens the code input menu
  public void OpenCodeInputMenu()
  {
    // Open the code input menu
    AppManager.Instance.OpenCodeInputMenu();
  }
}
