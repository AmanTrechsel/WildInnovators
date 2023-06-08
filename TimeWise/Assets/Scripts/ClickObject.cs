using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /* Copyright Xenfinity LLC - All Rights Reserved
     * Written by Bilal Itani
     * https://www.youtube.com/watch?v=EANtTI6BCxk
     */

public class ClickObject : MonoBehaviour
{
  [SerializeField]
  private Camera rayCamera;
  [SerializeField]
  private GameObject question;

  void Update()
  {
    if(Input.GetTouch(0).tapCount > 0)
    {
      RaycastHit hit;
      // This will turn a screen into a ray
      Ray ray = rayCamera.ScreenPointToRay(Input.GetTouch(0).position);

      // hit is used to send information into a variable without returning
      // out will store the information from this ray cast into the other ray cast
      // 100.0f is the border how far the sufficient is
      if(Physics.Raycast(ray, out hit, 100.0f))
      {
        if(hit.transform != null)
        {
          question.SetActive(true);
        }
      }
    }
  }
    
  // setActive is going to be used here
}
