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

    void Start()
    {
      // Calls the method at the bottom of this script
      AddCollider();
    }

    void Update()
    {
      // If there has been a touch
      if(Input.GetTouch(0).phase == TouchPhase.Began)
      {
        RaycastHit hit;

        // Raycast at the position of the touch-input
        Ray ray = cam.ScreenPointToRay(Input.GetTouch(0).position);

        // If the raycast hits a collider within 10000.0f units
        if(Physics.Raycast(ray, out hit, 10000.0f))
        {
          if(hit.transform != null)
          {
            // Show the question
            question.SetActive(true);
          }
        }
      }
    }

    // Adds a SphereCollider to every loaded GameObject
    public bool AddCollider()
    {
      List<GameObject> arObjects = new List<GameObject>();

      // Get the loadedObjects from ARCurson.cs
      foreach(GameObject loadedObject in ARCursor.Instance.arRepositionObjects)
      {
        // Adds a SphereCollider
        loadedObject.AddComponent<SphereCollider>();
      }
      return true;
    }
}
