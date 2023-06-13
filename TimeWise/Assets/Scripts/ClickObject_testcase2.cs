using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickObject_testcase2 : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            // This will turn a screen into a ray

            // beter een private camera hier zetten
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            // hit is used to send information into a variable without returning
            // out will store the information from this ray cast into the other ray cast
            // 100.0f is the border how far the sufficient is
            if(Physics.Raycast(ray, out hit, 100.0f))
            {
                if(hit.transform != null)
                {
                    PrintName(hit.transform.gameObject);
                }
            }
        }
    }
    
    void PrintName(GameObject ball)
    {
        print(ball.name);
    }
}
