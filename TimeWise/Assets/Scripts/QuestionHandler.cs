using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionHandler : MonoBehaviour
{
  public static QuestionHandler Instance;
  
  public GameObject popUpContent, question;

  [SerializeField]
  private Transform parent;

  void Awake()
  {
    if (Instance == null) { Instance = this; }
    else if (Instance != this) { Destroy(gameObject); }
  }

  void Start()
  {
    popUpContent = question;
    popUpContent.transform.SetParent(parent);
    HidePopUp();
  }

  public void CorrectAnswer(GameObject correctPopUp)
  {
    popUpContent = correctPopUp;
    popUpContent.SetActive(true);
  }

  public void IncorrectAnswer(GameObject incorrectPopUp)
  {
    popUpContent = incorrectPopUp;
    popUpContent.SetActive(true);
  }

  public void HidePopUp()
  {
    popUpContent.SetActive(false);
  }


}
