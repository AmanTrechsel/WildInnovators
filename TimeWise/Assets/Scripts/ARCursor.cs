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
  private List<GameObject> arRepositionObjects;
  private List<Renderer> arRepositionMeshes;
  private List<List<Vector3>> arRepositionOffsets;
  private bool arPlacedCorrectly;
  private bool subjectSet;

  private void Start()
  {
    
    if (AppManager.Instance.arSubject == null)
    {
      // Create the AR Object based on the object set in the App Manager
      arObject = Instantiate(AppManager.Instance.arDisplayObject, transform.position, transform.rotation) as GameObject;

      // Get its mesh renderer
      arMesh = arObject.GetComponentInChildren<Renderer>();

      subjectSet = false;
    }
    else
    {
      arRepositionObjects = new List<GameObject>();
      arRepositionMeshes = new List<Renderer>();
      arRepositionOffsets = new List<List<Vector3>>();
      foreach (ARItem arItem in AppManager.Instance.arSubject.items)
      {
        GameObject arObjectToAdd = Instantiate(arItem.prefab,
                                               transform.position + arItem.offsetPosition,
                                               transform.rotation * Quaternion.Euler(arItem.offsetRotation.x, arItem.offsetRotation.y, arItem.offsetRotation.z)
                                               ) as GameObject;
        arObjectToAdd.transform.localScale = arItem.offsetScale;
        Renderer[] arObjectMeshes = arObjectToAdd.GetComponentsInChildren<Renderer>();
        List<float> meshExtent = new List<float>();
        foreach (Renderer arMesh in arObjectMeshes)
        {
          meshExtent.Add(arMesh.bounds.extents.z);
        }
        if (arItem.recenter)
        {
          arRepositionObjects.Add(arObjectToAdd);
          arRepositionMeshes.Add(arObjectMeshes[0]);
          List<Vector3> arObjectOffsets = new List<Vector3>();
          arObjectOffsets.Add(arItem.offsetPosition);
          arObjectOffsets.Add(arItem.offsetRotation);
          //arObjectOffsets.Add(arItem.offsetScale);
          arRepositionOffsets.Add(arObjectOffsets);
        }

        // Add the nametag to the object
        if (arObjectMeshes.Length > 0)
        {
          GameObject nameTag = Instantiate(ARManager.Instance.nameTagPrefab) as GameObject;
          nameTag.GetComponent<NameTagManager>().SetupNametag(ARManager.Instance.arCamera, arItem.name);
          nameTag.transform.SetParent(arObjectToAdd.transform);
          //nameTag.transform.localScale = arItem.offsetScale;
          float topPosition = ((Mathf.Max(meshExtent.ToArray()) * arObjectToAdd.transform.localScale.z) + arObjectToAdd.transform.position.z) / 2f;
          topPosition = 0.1f; // Debug
          nameTag.transform.localPosition = new Vector3(0f, topPosition, 0f);
        }
        
      }

      subjectSet = true;
    }

    RepositionARObject();
  }

  private void Update()
  {
    // Cursor update
    UpdateCursor();

    // When the AR Object is no longer visible, try to reposition it.
    if (true)//(!ARMeshVisible() || ShouldRecenter()) && arPlacedCorrectly)
    {
      RepositionARObject();
    }

    // Only show the AR Object if it is placed correctly
    if (arObject != null) { arObject.SetActive(arPlacedCorrectly); }
  }

  // This script checks the position of an ARCard and repositions the AR Object accordingly
  private void RepositionARObject()
  {
    if (subjectSet)
    {
      for (int i = 0; i < arRepositionObjects.Count; i++)
      {
        GameObject repositionObject = arRepositionObjects[i];
        Vector3 rotationOffset = arRepositionOffsets[i][1];
        repositionObject.transform.position = transform.position + arRepositionOffsets[i][0];
        repositionObject.transform.rotation = transform.rotation * Quaternion.Euler(rotationOffset.x, rotationOffset.y, rotationOffset.z);
      }
    }
    else
    {
      arObject.transform.position = transform.position;
      arObject.transform.rotation = transform.rotation;
    }
  }

  // This script simply places a cursor on the center of the screen, which is used to reposition the AR Object
  // If there is none found it will set the 'arPlacedCorrectly' variable to false
  private void UpdateCursor()
  {
    return;
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

  private bool ShouldRecenter()
  {
    if (arRepositionMeshes == null) { return false; }
    foreach (Renderer renderer in arRepositionMeshes)
    {
      if (!renderer.isVisible) { return true; }
    }
    return false;
  }

  private bool ARMeshVisible()
  {
    if (arMesh == null) { return true; }
    return arMesh.isVisible;
  }
}
