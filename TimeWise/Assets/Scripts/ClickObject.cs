using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;
using System.Linq;

public class ClickObject : MonoBehaviour
{
  // VARIABLES //
  // Defining the camera to use
  [SerializeField]
  private Camera cam;
  // Defining the GameObject containing the pop-up with the question
  [SerializeField]
  private GameObject question;
  // Defining a list of every 3D model in the current scene
  [SerializeField]
  private List<GameObject> loadedObjects;

  // Defining a list for positions and create some values used for raycast-simulating
  private List<Vector3> objectPositions;
  private float range = 100.0f;
  private float zValue;

  void Start()
  {
    // Creating the lists
    loadedObjects = ARCursor.Instance.arRepositionObjects;
    objectPositions = new List<Vector3>();
  }

  void Update()
  {
    // Detects whether the screen has been touched
    if (Input.GetTouch(0).phase == TouchPhase.Began)
    {
      // Get the touch
      Touch touch = Input.GetTouch(0);
    
      // Get the positions of every 3D model and add them to a list
      foreach (GameObject loadedObject in loadedObjects)
      {
        Vector3 pos = loadedObject.transform.position;
        objectPositions.Add(pos);
      }

      // Go through the list and simulate a raycast (see method below)
      foreach(Vector3 objectPosition in objectPositions)
      {
        if(RaycastSimulation(objectPosition) == true)
        {
          question.SetActive(true);
        }
      }
    }
  }

  // Method for simulating a raycast
  private bool RaycastSimulation(Vector3 objectPosition)
  {
    // Turn the z-value of a loaded object to a float with 1 decimal point
    float objectZValue = (float)System.Math.Round(objectPosition.z, 1);

    // Get the touch position and transform it to coordinates in world space
    Vector3 touchInWorld = cam.ScreenToWorldPoint(Input.GetTouch(0).position);

    // Go through different values of z and check whether an object is within certain boundaries around each z value
    for(float i = 0; i <= 1000.0f; i += 0.1f)
    {
      zValue = i;
      Vector3 worldPoint = new Vector3(touchInWorld.x, touchInWorld.y, zValue);
      if(objectZValue == worldPoint.z && Vector2.Distance(new Vector2(objectPosition.x, objectPosition.y), new Vector2(worldPoint.x, worldPoint.y)) <= range)
      {
        return true;
      }
    }
    return false;
  }
}
