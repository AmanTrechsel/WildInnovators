using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CourseSelectionButton : MonoBehaviour
{
  [SerializeField]
  private TextMeshProUGUI buttonName;
  [SerializeField]
  private Image buttonImage;

  private Course _course;

  public void SetCourse(Course newCourse)
  {
    _course = newCourse;
    buttonImage.sprite = _course.buttonGraphic;
    buttonName.text = _course.name;
  }

  public void GoToCourse()
  {
    AppManager.Instance.selectedCourse = _course;
    AppManager.Instance.ChangeScene("Selection");
  }
}
