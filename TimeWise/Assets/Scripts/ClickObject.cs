using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickObject : MonoBehaviour
{
  public GameObject model;

  void Update()
  {
    if (Input.GetMouseButtonDown(0)) { 
        if (model == getClickedObject(out RaycastHit hit))
        {
          print("clicked/ object is touched"); 
        }
        else if (model != getClickedObject(out RaycastHit hit2))
        {
          print("bla");
        }
    }

    if (Input.GetMouseButtonUp(0)) { print("click is off"); }
  }

  // Mark - Get clicked object 
  // https://www.youtube.com/watch?v=pfkQDGhd8_A&t=63s

  GameObject getClickedObject (out RaycastHit hit)
  {
    GameObject target = null;

    Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

    if (Physics.Raycast(ray.origin, ray.direction = new Vector3(0f, 0f, 10f), out hit))
    {
      if (!isPointerOverUIObject()) { target = hit.collider.gameObject; }
    }
    return target;
  }

  private bool isPointerOverUIObject()
  {
    bool check;

    PointerEventData ped = new PointerEventData(EventSystem.current);
    ped.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    List<RaycastResult> results = new List<RaycastResult>();
    EventSystem.current.RaycastAll(ped, results);

    if (results.Count == 0)
    {
        check = false;
    } 
    else
    {
        check = true;
    }

    return check;
  }
}
