using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class TouchManager : MonoBehaviour
{
  [SerializeField]
  private GameObject question;
  [SerializeField]
  private Transform parent;

  private PlayerInput playerInput;

  // private InputAction positionAction; 
  private InputAction pressAction;

  void Awake()
  {
    playerInput = GetComponent<PlayerInput>();
    // positionAction = playerInput.actions["TouchPosition"];
    pressAction = playerInput.actions["TouchPress"];
  }

  void OnEnable()
  {
    pressAction.performed += TouchPressed;
  }

  void OnDisable()
  {
    pressAction.performed -= TouchPressed;
  }

  void TouchPressed(InputAction.CallbackContext context)
  {
    // Vector3 position = Camera.main.ScreenToWorldPoint(positionAction.ReadValue<Vector2>());
    // position.z = model.transform.position.z;
    // model.transform.position = position;

    Instantiate(question, question.transform.position, question.transform.rotation, parent);
  }

  // Start of a method to change the content of the prefab. The prefab is going to require a script as well, which will deliver all the parameters.
  
  /*private void ChangePrefab(TextMeshProUGUI title, TextMeshProUGUI question, Button option1, Button option2, Button option3)
  {
    title.text = title;
    question.text = question;
    option1.text = option1;
    option2.text = option2;
    option3.text = option3;
  }*/
}

