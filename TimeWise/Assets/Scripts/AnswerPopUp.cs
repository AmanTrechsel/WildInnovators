using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerPopUp : MonoBehaviour
{
  public void Correct(GameObject correctPrefab)
  {
    QuestionHandler.Instance.CorrectAnswer(correctPrefab);
  }

  public void Incorrect(GameObject incorrectPrefab)
  {
    QuestionHandler.Instance.IncorrectAnswer(incorrectPrefab);
  }
}
