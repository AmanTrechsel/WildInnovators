using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionHandler : MonoBehaviour
{
    public void QuitButton()
    {
        TouchManager.Instance.DestroyObject();
    }
}
