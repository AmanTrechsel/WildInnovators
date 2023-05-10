using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARCursor : MonoBehaviour
{
  [SerializeField]
  private ARRaycastManager raycastManager;

  private GameObject arObject;
  private Renderer arMesh;
  private bool arPlacedCorrectly;

  void Start()
  {
    // Create the AR Object based on the object set in the App Manager
    arObject = Instantiate(AppManager.appManager.arDisplayObject, transform.position, transform.rotation) as GameObject;

    // Get its mesh renderer
    arMesh = arObject.GetComponentInChildren<Renderer>();

    RepositionARObject();
  }

  void Update()
  {
    // Cursor update
    UpdateCursor();

    // When the AR Object is no longer visible, try to reposition it.
    if (!arMesh.isVisible && arPlacedCorrectly)
    {
      RepositionARObject();
    }

    // Only show the AR Object if it is placed correctly
    arObject.SetActive(arPlacedCorrectly);
  }

  // This script checks your current AR plane for possible placement positions and repositions the AR Object accordingly
  void RepositionARObject()
  {
    List<ARRaycastHit> hits = new List<ARRaycastHit>();
    raycastManager.Raycast(transform.position, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);
    if (hits.Count > 0)
    {
      arObject.transform.position = transform.position;
      arObject.transform.rotation = transform.rotation;
    }
  }

  // This script simply places a cursor on the center of the screen, which is used to reposition the AR Object
  // If there is none found it will set the 'arPlacedCorrectly' variable to false
  void UpdateCursor()
  {
    Vector2 screenPosition = ARManager.Instance.arCamera.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
    List<ARRaycastHit> hits = new List<ARRaycastHit>();
    raycastManager.Raycast(screenPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

    if (hits.Count > 0)
    {
      arPlacedCorrectly = true;
      transform.position = hits[hits.Count-1].pose.position;
      transform.rotation = hits[hits.Count-1].pose.rotation;
    }
    else
    {
      arPlacedCorrectly = false;
    }
  }
}
