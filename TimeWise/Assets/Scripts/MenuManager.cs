using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public void GetIndex()
    {
        int index = transform.GetSiblingIndex();
        AppManager.Instance.arSubject = ResourceManager.Instance.GetSubjectByID(index);
        Debug.Log(index);
    }

    public void GoToARScene()
    {
        SceneManager.LoadScene("AR");
        Debug.Log(AppManager.Instance.arSubject);
    }

}
