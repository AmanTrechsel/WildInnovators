using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

public class ClickObject : MonoBehaviour
{
  [SerializeField]
  private ARRaycastManager raycastManager;
  // [SerializeField]
  // private Camera rayCamera;
  [SerializeField]
  private GameObject question;

  void Update()
  {
    /*if(Input.GetTouch(0).phase == TouchPhase.Began)
    {
      Touch touch = Input.GetTouch(0);
      // RaycastHit hit;
      // This will turn a screen into a ray
      Ray ray = rayCamera.ScreenPointToRay(touch.rawPosition);

      // hit is used to send information into a variable without returning
      // out will store the information from this ray cast into the other ray cast
      // 100.0f is the border how far the sufficient is
      if(Physics.Raycast(ray, out hit, 100.0f))
      {
        // if(hit.transform != null)
        // {
          question.SetActive(true);
        // }
      }
    }*/

    if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
    {
      List<ARRaycastHit> hits = new List<ARRaycastHit>();
      raycastManager.Raycast(Input.GetTouch(0).position, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);
      if (hits.Count > 0)
      {
          question.SetActive(true);
      }
    }
  }
}
