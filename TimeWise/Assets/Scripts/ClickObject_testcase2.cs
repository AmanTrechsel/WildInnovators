using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickObject_testcase2 : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private GameObject question;

    void Update()
    {
        if(Input.GetTouch(0).phase == TouchPhase.Began)       // Input.GetMouseButtonDown(0)
        {
            RaycastHit hit;

            Ray ray = cam.ScreenPointToRay(Input.GetTouch(0).position);     // Input.mousePosition

            if(Physics.Raycast(ray, out hit, 10000.0f))
            {
                if(hit.transform != null)
                {
                    PrintName(hit.transform.gameObject);
                    question.SetActive(true);
                }
            }
        }
    }
    
    // Method will be obsolete in the final version
    void PrintName(GameObject ball)
    {
        print(ball.name);
    }

    // The question is now hard-coded into the scene (it's already in the hierarchy), but we want the content of question change depending on which object has been clicked.
}
