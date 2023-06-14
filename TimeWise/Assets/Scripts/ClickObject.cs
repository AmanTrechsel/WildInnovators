using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickObject : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private GameObject question;

    [SerializeField]
    private TextMeshProUGUI screenText;

    void Start()
    {
      AddCollider(); 
    }

    void Update()
    {
      if(Input.GetTouch(0).phase == TouchPhase.Began)       // Input.GetMouseButtonDown(0)
      {
        RaycastHit hit;
        screenText.text = "Er is gedrukt";

        Ray ray = cam.ScreenPointToRay(Input.GetTouch(0).position);     // Input.mousePosition

        if(Physics.Raycast(ray, out hit, 10000.0f))
        {
          screenText.text = "Er is een collider geraakt";
          if(hit.transform != null)
          {
            question.SetActive(true);
          }
        }
      }
    }

    // Adds a SphereCollider to every loaded GameObject
    public bool AddCollider()
    {
      List<GameObject> arObjects = new List<GameObject>();

      foreach(GameObject loadedObject in ARCursor.Instance.arRepositionObjects)
      {
        loadedObject.AddComponent<SphereCollider>();
      }
      return true;
    }
}
