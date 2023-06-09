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
  // Defining variables
  // Defining the camera to use
  [SerializeField]
  private Camera cam;
  // Defining the GameObject containing the pop-up with the question
  [SerializeField]
  private GameObject question;
  // Defining a list of every 3D model in the current scene
  [SerializeField]
  private List<GameObject> loadedObjects;

  // Defining some lists for positions and distances of every 3D model in the scene
  private List<Vector3> objectPositions;
  private List<float> distances;

  void Start()
  {
    // Creating the lists
    loadedObjects = ARCursor.Instance.arRepositionObjects;
    objectPositions = new List<Vector3>();
    distances = new List<float>();
  }

  void Update()
  {
    // Detects whether the screen has been touched
    if (Input.GetTouch(0).phase == TouchPhase.Began)
    {
      // Get the touch and its position
      Touch touch = Input.GetTouch(0);
      Vector2 touchPosition = touch.position;

      // Get the positions of every 3D model in the scene and set it as screenpoints
      foreach (GameObject loadedObject in loadedObjects)
      {
        Vector3 pos = cam.WorldToScreenPoint(loadedObject.transform.position);
        Vector2 pos2D = new Vector2(pos.x, pos.y);
        objectPositions.Add(pos2D);
      }

      // Get the distances of every 3D model to the touch position
      foreach (Vector2 objectPos in objectPositions)
      {
        float distance = Vector2.Distance(touchPosition, objectPos);
        distances.Add(distance);
      }

      // Get the smallest distance
      float minimunDistance = distances.Min();

      // If the touch is close enough to the 3D model, show the question
      if (minimunDistance <= 1000000000000.0f)
      {
        question.SetActive(true);
      }
    }
  }
}
