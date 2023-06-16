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
    correctPopUp.SetActive(true);
    question.SetActive(false);
  }

  // If the button with the incorrect answer is pressed, show the incorrectPopUp
  public void IncorrectAnswer()
  {
    incorrectPopUp.SetActive(true);
    question.SetActive(false);
  }
}
