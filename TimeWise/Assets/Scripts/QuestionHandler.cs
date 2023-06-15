using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionHandler : MonoBehaviour
{
  // Singleton
  public static QuestionHandler Instance;
  
  public GameObject popUpContent, question;

  [SerializeField]
  private GameObject parent;

  void Awake()
  {
    if (Instance == null) { Instance = this; }
    else if (Instance != this) { Destroy(gameObject); }
  }

  void Start()
  {
    popUpContent = question;
  }

  // If the button with the correct answer is pressed, show the correctPopUp
  public void CorrectAnswer(GameObject correctPopUp)
  {
    popUpContent = correctPopUp;
    popUpContent.SetActive(true);
  }

  // If the button with the incorrect answer is pressed, show the incorrectPopUp
  public void IncorrectAnswer(GameObject incorrectPopUp)
  {
    popUpContent = incorrectPopUp;
    popUpContent.SetActive(true);
  }

  // Disables the pop-up
  public void HidePopUp()
  {
    popUpContent.SetActive(false);
  }


}
