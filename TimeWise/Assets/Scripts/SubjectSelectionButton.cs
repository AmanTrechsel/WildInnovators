using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SubjectSelectionButton : MonoBehaviour
{
  [SerializeField]
  private TextMeshProUGUI buttonName;
  [SerializeField]
  private Image buttonImage;

  private Subject _subject;

  public void SetSubject(Subject newSubject)
  {
    _subject = newSubject;

    buttonImage.sprite = _subject.buttonGraphic;
    buttonName.text = _subject.name;
  }

  public void SelectSubject()
  {
    MenuManager.Instance.SetSubjectByIndex((int)_subject.id);
    MenuManager.Instance.GoToARScene();
  }

  public void ShowInfoBox()
  {
    MenuManager.Instance.ShowInfoBox(_subject, transform.position);
  }
}
