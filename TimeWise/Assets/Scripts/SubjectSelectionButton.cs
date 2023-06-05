using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SubjectSelectionButton : MonoBehaviour
{
  // The text field for the button name
  [SerializeField]
  private TextMeshProUGUI buttonName;
  // The image for the button
  [SerializeField]
  private Image buttonImage;

  // The subject for the button
  private Subject _subject;

  // Set the subject for the button
  public void SetSubject(Subject newSubject)
  {
    // Set the subject, button image and name
    _subject = newSubject;
    buttonImage.sprite = _subject.buttonGraphic;
    buttonName.text = _subject.name;
  }

  // Select the subject
  public void SelectSubject()
  {
    // Set the subject and go to the AR scene
    MenuManager.Instance.SetSubjectByIndex((int)_subject.id);
    MenuManager.Instance.GoToARScene();
  }

  // Show the info box
  public void ShowInfoBox()
  {
    // Call the ShowInfoBox method in MenuManager.cs
    MenuManager.Instance.ShowInfoBox(_subject, transform.position);
  }
}
