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

  // The offset for the info box
  [SerializeField]
  private Vector3 infoOffset;
  // The info box and subject button prefab
  [SerializeField]
  private GameObject info, subjectButtonPrefab;
  // The content rect transform
  [SerializeField]
  private RectTransform contentRect;
  // Layout group for the content
  [SerializeField]
  private VerticalLayoutGroup contentLayoutGroup;
  // The content transforms for the subject buttons and info box
  [SerializeField]
  private Transform subjectButtonContent, infoContent;
  // The text fields for the subject name, subject content and course name
  [SerializeField]
  private TextMeshProUGUI subjectName, subjectContent, courseName;
  // Encyclopedia unlock count text field
  [SerializeField]
  private TextMeshProUGUI encyclopediaUnlockCount;

  // Called once at the start of the app
  private void Awake()
  {
    // Assign this instance as the singleton
    if (Instance == null) { Instance = this; }
    else if (Instance != this) { Destroy(gameObject); }

    // Set the course name
    courseName.text = AppManager.Instance.selectedCourse.name;

    // Hide the info box
    HideInfoBox();

    // Iterate through all subjects found in the selected course
    foreach (Subject _subject in AppManager.Instance.selectedCourse.subjects)
    {
      // Create a new subject button based on the given prefab
      GameObject subjectButton = Instantiate(subjectButtonPrefab) as GameObject;
      subjectButton.GetComponent<SubjectSelectionButton>().SetSubject(_subject);
      subjectButton.transform.SetParent(subjectButtonContent);
    }

    // Setup the content's height with the padding
    float contentHeight = contentLayoutGroup.padding.top + contentLayoutGroup.padding.bottom;

    // Update the content's height to be the sum of all the items' heights
    foreach (RectTransform child in subjectButtonContent)
    {
      // Add the height of the child along with the padding multiplied by its scale to the content's height
      contentHeight += (child.sizeDelta.y + contentLayoutGroup.spacing) * child.localScale.y;
    }

    // 130% multiplier to the content's height
    contentHeight *= 1.3f;

    // Set the content's height to contentHeight
    contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, contentHeight);

    // Set the encyclopedia unlock count
    encyclopediaUnlockCount.text = AppManager.Instance.GetEncyclopediaUnlockText();
  }

  // Method to select the subject
  public void SetSubjectByIndex(int index)
  {
    // Set the subject in AppManager to the subject selected
    AppManager.Instance.arSubject = ResourceManager.Instance.GetSubjectByID(index);
  }

  // Switches to the ARScene, which will contain the subject selected at the function above here.
  public void GoToARScene()
  {
    // Load the AR scene
    AppManager.Instance.GoToARScene();
  }

  // Shows the info box, fills in the data for the info and repositions it.
  public void ShowInfoBox(Subject subject, Vector3 subjectButtonPosition)
  {
    // Set the subject name and content
    subjectName.text = subject.name;
    subjectContent.text = subject.infoContent;
    infoContent.position = subjectButtonPosition + infoOffset;

    // Show the info box
    info.SetActive(true);
  }

  // Hides the info box
  public void HideInfoBox()
  {
    // Hide the info box
    info.SetActive(false);
  }

  // Basic method for opening the encyclopedia
  public void OpenEncyclopedia()
  {
    // Open the encyclopedia
    AppManager.Instance.GoToEncyclopedia();
  }
}
