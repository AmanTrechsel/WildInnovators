using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionHandler : MonoBehaviour
{
  // Singleton
  public static QuestionHandler Instance;
  
  [SerializeField]
  private GameObject question, correctPopUp, incorrectPopUp;

  void Awake()
  {
    if (Instance == null) { Instance = this; }
    else if (Instance != this) { Destroy(gameObject); }
  }

  // If the button with the correct answer is pressed, show the correctPopUp
  public void CorrectAnswer()
  {
    CloseQuestion(correctPopUp);

    // Check every encyclopedia page with this subject
    foreach (EncyclopediaPage encyclopediaPage in ResourceManager.Instance.GetEncyclopediaPagesBySubject(AppManager.Instance.arSubject))
    {
      // Check if this page isn't already unlocked
      if (!AppManager.Instance.unlockedEncyclopediaPages.Contains((int)encyclopediaPage.id))
      {
        // Unlock this page
        AppManager.Instance.unlockedEncyclopediaPages.Add((int)encyclopediaPage.id);
      }
    }
  }

  // If the button with the incorrect answer is pressed, show the incorrectPopUp
  public void IncorrectAnswer()
  {
    CloseQuestion(incorrectPopUp);
  }

  // Closes the question pop up
  public void CloseQuestion(GameObject popUp)
  {
    popUp.SetActive(true);
    question.SetActive(false);
  }
}
