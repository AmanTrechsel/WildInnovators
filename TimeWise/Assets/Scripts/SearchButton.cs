using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SearchButton : MonoBehaviour
{
  [SerializeField]
  private TextMeshProUGUI buttonName;
  [SerializeField]
  private Image buttonImage;

  public enum ButtonType {
    Course = 0,
    Subject = 1,
    Setting = 2,
    Encyclopedia = 3
  }

  private ButtonType buttonType;
  private int id;

  public void SetCourse(Course course)
  {
    buttonImage.sprite = course.buttonGraphic;
    buttonName.text = course.name;
    id = (int)course.id;

    buttonType = ButtonType.Course;
  }

  public void SetSubject(Subject subject)
  {
    buttonImage.sprite = subject.buttonGraphic;
    buttonName.text = subject.name;
    id = (int)subject.id;

    buttonType = ButtonType.Subject;
  }

  public void SetSetting(Setting setting)
  {
    buttonImage.sprite = setting.buttonGraphic;
    buttonName.text = setting.name;
    id = (int)setting.id;

    buttonType = ButtonType.Setting;
  }

  public void SetPage(EncyclopediaPage page)
  {
    if (AppManager.Instance.unlockedEncyclopediaPages.Contains((int)page.id))
    {
      buttonName.text = page.displayName;
      buttonImage.sprite = page.displayImage;
    }
    else
    {
      buttonName.text = "???";
      buttonImage.enabled = false;
    }
    id = (int)page.id;

    buttonType = ButtonType.Encyclopedia;
  }

  public void PressButton()
  {
    switch (buttonType)
    {
      case ButtonType.Course:
        AppManager.Instance.selectedCourse = ResourceManager.Instance.GetCourseByID(id);
        AppManager.Instance.LoadScene("Selection");
        break;
      case ButtonType.Subject:
        AppManager.Instance.arSubject = ResourceManager.Instance.GetSubjectByID(id);
        AppManager.Instance.GoToARScene();
        break;
      case ButtonType.Setting:
        AppManager.Instance.LoadScene("Settings");
        break;
      case ButtonType.Encyclopedia:
        EncyclopediaPage myPage = ResourceManager.Instance.GetEncyclopediaPageByID(id);
        Subject mySubject = myPage.subject;
        AppManager.Instance.arSubject = mySubject;
        AppManager.Instance.selectedCourse = ResourceManager.Instance.FindCourseContainingSubject(mySubject);
        AppManager.Instance.LoadScene("Encyclopedia");
        break;
    }
  }
}