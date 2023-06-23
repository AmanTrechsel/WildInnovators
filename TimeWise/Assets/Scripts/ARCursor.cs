using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARCursor : MonoBehaviour
{
  // Singleton
  public static ARCursor Instance;

  // Holds the ARRaycastManager, which is used to find the AR plane
  [SerializeField]
  private ARRaycastManager raycastManager;
  // Objects that may need to be repositioned
  public List<GameObject> arRepositionObjects;
  // Meshes that may need to be repositioned
  private List<Renderer> arRepositionMeshes;
  // Offsets for the repositioned objects
  private List<List<Vector3>> arRepositionOffsets;

  private void Start()
  {
    if (Instance == null) { Instance = this; }
    else if (Instance != this) { Destroy(gameObject); }

    // Initialize lists for repositionable objects
    arRepositionObjects = new List<GameObject>();
    arRepositionMeshes = new List<Renderer>();
    arRepositionOffsets = new List<List<Vector3>>();
    // Iterate through the AR Items and add them to the scene
    foreach (ARItem arItem in AppManager.Instance.arSubject.items)
    {
      // Instantiate the AR Object
      GameObject arObjectToAdd = Instantiate(arItem.prefab,
                                              transform.position + arItem.offsetPosition,
                                              transform.rotation * Quaternion.Euler(arItem.offsetRotation.x, arItem.offsetRotation.y, arItem.offsetRotation.z)
                                              ) as GameObject;
      // Apply the offset scale
      arObjectToAdd.transform.localScale = arItem.offsetScale;
      // Add the AR Object's meshes to the reposition list
      Renderer[] arObjectMeshes = arObjectToAdd.GetComponentsInChildren<Renderer>();
      // Initialize the mesh extent list
      List<float> meshExtent = new List<float>();
      // Iterate through the meshes and add their extents to the list
      foreach (Renderer arMesh in arObjectMeshes)
      {
        meshExtent.Add(arMesh.bounds.extents.z);
      }
      // Add the object to the reposition lists
      arRepositionObjects.Add(arObjectToAdd);
      arRepositionMeshes.Add(arObjectMeshes[0]);
      List<Vector3> arObjectOffsets = new List<Vector3>();
      arObjectOffsets.Add(arItem.offsetPosition);
      arObjectOffsets.Add(arItem.offsetRotation);
      arRepositionOffsets.Add(arObjectOffsets);

      // Add the nametag and box collider to the object
      if (arObjectMeshes.Length > 0)
      {
        // Instantiate the nametag
        GameObject nameTag = Instantiate(ARManager.Instance.nameTagPrefab) as GameObject;
        // Setup the nametag
        nameTag.GetComponent<NameTagManager>().SetupNametag(ARManager.Instance.arCamera, arItem.name);
        nameTag.transform.SetParent(arObjectToAdd.transform);
        // Calculate the top position of the object
        float topPosition = ((Mathf.Max(meshExtent.ToArray()) * arObjectToAdd.transform.localScale.z) + arObjectToAdd.transform.position.z) / 2f;
        topPosition = 0.1f; // Debug
        // Set the nametag position
        nameTag.transform.localPosition = new Vector3(0f, topPosition, 0f);

        // Get the largest renderer in the object
        Renderer objectRenderer = arObjectMeshes[0];
        foreach (Renderer arMesh in arObjectMeshes)
        {
          if (arMesh.bounds.extents.z > objectRenderer.bounds.extents.z)
          {
            objectRenderer = arMesh;
          }
        }
        // Add a mesh collider to the object
        MeshCollider objectCollider = arObjectToAdd.AddComponent<MeshCollider>();
        // Check if the object has a mesh filter
        if (objectRenderer.GetComponent<MeshFilter>() != null)
        {
          // Set the collider mesh to the object's mesh
          objectCollider.sharedMesh = objectRenderer.GetComponent<MeshFilter>().sharedMesh;
        }
        else
        {
          // Make object renderer a SkinnedMeshRenderer
          SkinnedMeshRenderer objectSkinnedRenderer = objectRenderer as SkinnedMeshRenderer;
          // Set the collider mesh to the object's renderer mesh
          objectCollider.sharedMesh = objectSkinnedRenderer.sharedMesh;
        }
        // Make the collider convex
        objectCollider.convex = true;
      }
    }

    // Reposition AR Objects
    RepositionARObject();
  }

  private void Update()
  {
    // Reposition AR Objects
    RepositionARObject();

    // Hide AR Objects during calibration
    foreach (GameObject arRepositionObject in arRepositionObjects)
    {
      // Hide the object if calibration is active
      arRepositionObject.SetActive(!ARManager.Instance.calibrating);
    }
  }

  // This script checks the position of an ARCard and repositions the AR Object accordingly
  private void RepositionARObject()
  {
    // Iterate through the repositionable objects
    for (int i = 0; i < arRepositionObjects.Count; i++)
    {
      // Reposition the object
      GameObject repositionObject = arRepositionObjects[i];
      Vector3 rotationOffset = arRepositionOffsets[i][1];
      repositionObject.transform.position = transform.position + arRepositionOffsets[i][0];
      repositionObject.transform.rotation = transform.rotation * Quaternion.Euler(rotationOffset.x, rotationOffset.y, rotationOffset.z);
    }
  }
}
