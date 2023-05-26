using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
  // Singleton
  public static MenuManager Instance;

  [SerializeField]
  private Vector3 infoOffset;
  [SerializeField]
  private GameObject info, subjectButtonPrefab;
  [SerializeField]
  private Transform subjectButtonContent, infoContent;
  [SerializeField]
  private TextMeshProUGUI subjectName, subjectContent, courseName;

  // Called once at the start of the app
  private void Awake()
  {
    // Assign this instance as the singleton
    if (Instance == null) { Instance = this; }

    courseName.text = AppManager.Instance.selectedCourse.name;

    HideInfoBox();

    foreach (Subject _subject in AppManager.Instance.selectedCourse.subjects)
    {
      GameObject subjectButton = Instantiate(subjectButtonPrefab) as GameObject;
      subjectButton.GetComponent<SubjectSelectionButton>().SetSubject(_subject);
      subjectButton.transform.SetParent(subjectButtonContent);
    }
  }

  // Method to select the subject
  public void SetSubjectByIndex(int index)
  {
    AppManager.Instance.arSubject = ResourceManager.Instance.GetSubjectByID(index);
  }

  // Switches to the ARScene, which will contain the subject selected at the function above here.
  public void GoToARScene()
  {
    SceneManager.LoadScene("AR");

    foreach (EncyclopediaPage encyclopediaPage in ResourceManager.Instance.GetEncyclopediaPagesBySubject(AppManager.Instance.arSubject))
    {
      AppManager.Instance.unlockedEncyclopediaPages.Add((int)encyclopediaPage.id);
    }
  }

  // Shows the info box, fills in the data for the info and repositions it.
  public void ShowInfoBox(Subject subject, Vector3 subjectButtonPosition)
  {
    subjectName.text = subject.name;
    subjectContent.text = subject.infoContent;
    infoContent.position = subjectButtonPosition + infoOffset;
    info.SetActive(true);
  }

  public void HideInfoBox()
  {
    info.SetActive(false);
  }
}
