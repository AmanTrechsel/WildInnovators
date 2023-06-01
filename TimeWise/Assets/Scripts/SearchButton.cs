using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SearchButton : MonoBehaviour
{
  // The text field for the button name
  [SerializeField]
  private TextMeshProUGUI buttonName;
  // The image for the button
  [SerializeField]
  private Image buttonImage;

  // An enum for the button type
  public enum ButtonType {
    Course = 0,
    Subject = 1,
    Setting = 2,
    Encyclopedia = 3
  }

  // The button type
  private ButtonType buttonType;
  // The id of the button
  private int id;

  // Set the button to a course
  public void SetCourse(Course course)
  {
    // Set the button image, name and id
    buttonImage.sprite = course.buttonGraphic;
    buttonName.text = course.name;
    id = (int)course.id;

    // Set the button type
    buttonType = ButtonType.Course;
  }

  // Set the button to a subject
  public void SetSubject(Subject subject)
  {
    // Set the button image, name and id
    buttonImage.sprite = subject.buttonGraphic;
    buttonName.text = subject.name;
    id = (int)subject.id;

    // Set the button type
    buttonType = ButtonType.Subject;
  }

  // Set the button to a setting
  public void SetSetting(Setting setting)
  {
    // Set the button image, name and id
    buttonImage.sprite = setting.buttonGraphic;
    buttonName.text = setting.name;
    id = (int)setting.id;

    // Set the button type
    buttonType = ButtonType.Setting;
  }

  // Set the button to an encyclopedia page
  public void SetPage(EncyclopediaPage page)
  {
    // Check if the page is unlocked
    if (AppManager.Instance.unlockedEncyclopediaPages.Contains((int)page.id))
    {
      // Set the button image and name to the page
      buttonName.text = page.displayName;
      buttonImage.sprite = page.displayImage;
    }
    else
    {
      // Set the button name to ??? and disable the button image
      buttonName.text = "???";
      buttonImage.enabled = false;
    }

    // Set the button id
    id = (int)page.id;

    // Set the button type
    buttonType = ButtonType.Encyclopedia;
  }

  // When the button is pressed
  public void PressButton()
  {
    // Check the button type and load the appropriate scene
    switch (buttonType)
    {
      case ButtonType.Course:
        // Set the selected course and load the selection scene
        AppManager.Instance.selectedCourse = ResourceManager.Instance.GetCourseByID(id);
        AppManager.Instance.LoadScene("Selection");
        break;
      case ButtonType.Subject:
        // Set the ar subject and load the ar scene
        AppManager.Instance.arSubject = ResourceManager.Instance.GetSubjectByID(id);
        AppManager.Instance.GoToARScene();
        break;
      case ButtonType.Setting:
        // Load the settings scene
        AppManager.Instance.LoadScene("Settings");
        break;
      case ButtonType.Encyclopedia:
        // Set the ar subject and load the encyclopedia scene
        EncyclopediaPage myPage = ResourceManager.Instance.GetEncyclopediaPageByID(id);
        Subject mySubject = myPage.subject;
        AppManager.Instance.arSubject = mySubject;
        AppManager.Instance.selectedCourse = ResourceManager.Instance.FindCourseContainingSubject(mySubject);
        AppManager.Instance.LoadScene("Encyclopedia");
        break;
    }
  }
}