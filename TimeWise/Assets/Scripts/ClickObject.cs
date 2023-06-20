using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickObject : MonoBehaviour
{
  [SerializeField]
  private Camera cam;

  // Contains the question gameobject
  [SerializeField]
  private GameObject question;

  void Update()
  {
    // Check if the user taps the screen
    if (Input.touchCount > 0)
    {
      // Store current touch
      Touch inputTouch = Input.GetTouch(0);

      // If there was a valid touch
      if (inputTouch.phase == TouchPhase.Began)
      {
        RaycastHit hit;

        // If the spherecast hits a collider within 10000.0f units and a radius based on the touch multiplied by factor 2
        if (Physics.SphereCast(inputTouch.position, inputTouch.radius * 2, cam.transform.forward, out hit, 10000.0f))
        {
          if (hit.transform != null)
          {
            // Show the question
            question.SetActive(true);

            // Hide the tooltip for clicking an object
            ARManager.Instance.HideTooltip();

            // Makes it so you don't get the tooltip again
            AppManager.Instance.hasClicked = true;
          }
        }
      }
    }
  }
}
