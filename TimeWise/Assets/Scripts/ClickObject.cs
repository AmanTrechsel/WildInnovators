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

    // Adds a BoxCollider to every loaded GameObject
    public void AddCollider()
    {
      Debug.Log(ARCursor.Instance.arRepositionObjects);
      // Get the loadedObjects from ARCursor.cs
      foreach(GameObject loadedObject in ARCursor.Instance.arRepositionObjects)
      {
        // Adds a BoxCollider
        BoxCollider bc = loadedObject.AddComponent<BoxCollider>() as BoxCollider;
        Renderer objectRenderer = GetComponentInChildren<Renderer>();
        Bounds bounds = objectRenderer.bounds;
        bc.center = bounds.center;
        bc.size = bounds.extents; 
      }
    }
}
