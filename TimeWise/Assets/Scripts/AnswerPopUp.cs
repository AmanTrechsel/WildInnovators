using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerPopUp : MonoBehaviour
{
  // Calls the method in QuestionHandler.cs
  public void Correct(GameObject correctPrefab)
  {
    QuestionHandler.Instance.CorrectAnswer(correctPrefab);
  }
  
  // Calls the method in QuestionHandler.cs
  public void Incorrect(GameObject incorrectPrefab)
  {
    QuestionHandler.Instance.IncorrectAnswer(incorrectPrefab);
  }
}
