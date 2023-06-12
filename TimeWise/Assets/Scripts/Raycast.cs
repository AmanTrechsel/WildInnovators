using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;
using System.Linq;

public class Raycast : MonoBehaviour
{
  [SerializeField]
  private ARRaycastManager raycastManager;
  [SerializeField]
  private Camera cam;
  [SerializeField]
  private GameObject question;

  public List<GameObject> loadedObjects;

  private List<Vector2> objectPositions;
  private float range = 100.0f;

  void Start()
  {
    loadedObjects = ARCursor.Instance.arRepositionObjects;
    objectPositions = new List<Vector2>();
  }

  void Update()
  {
    Touch touch = Input.GetTouch(0);

    foreach(GameObject loadedObject in loadedObjects)
    {
      Vector3 pos = cam.WorldToScreenPoint(loadedObject.transform.position);
      Vector2 pos2D = new Vector2(pos.x, pos.y);
      objectPositions.Add(pos2D);
    }

    if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
    {
      List<ARRaycastHit> hits = new List<ARRaycastHit>();

      if(raycastManager.Raycast(touch.position, hits))
      {
        foreach(ARRaycastHit hit in hits)
        {
          Vector2 rayPosition = new Vector2(hit.pose.position.x, hit.pose.position.y);
          foreach(Vector2 objectPosition in objectPositions)
          {
            if(Vector2.Distance(objectPosition, rayPosition) <= range)
            {
              question.SetActive(true);
            }
          }
        }
      }
    }
  }
}
