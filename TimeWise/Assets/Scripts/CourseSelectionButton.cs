using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CourseSelectionButton : MonoBehaviour
{
  // The text field for the button name
  [SerializeField]
  private TextMeshProUGUI buttonName;
  // The image for the button graphic
  [SerializeField]
  private Image buttonImage;

  // The course for the button
  private Course _course;

  // Set the course for the button
  public void SetCourse(Course newCourse)
  {
    // Assign the course
    _course = newCourse;
    // Set the button graphic and name
    buttonImage.sprite = _course.buttonGraphic;
    buttonName.text = _course.name;
  }

  // Go to the course
  public void GoToCourse()
  {
    // Set the selected course and change the scene to the selection scene
    AppManager.Instance.selectedCourse = _course;
    AppManager.Instance.ChangeScene("Selection");
  }
}
