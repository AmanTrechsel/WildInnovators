using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class TouchManager : MonoBehaviour
{
  // Singleton
  public static TouchManager Instance;

  [SerializeField]
  private GameObject question;
  [SerializeField]
  private Transform parent;
  // reference
  private PlayerInput playerInput;
  // reference in TouchAction (Input Actions)
  private InputAction pressAction;

  void Awake()
  {
    if (Instance == null) { Instance = this; }
    else if (Instance != this) { Destroy(gameObject); }

    playerInput = GetComponent<PlayerInput>();

    pressAction = playerInput.actions["TouchPress"];

    foreach(Transform child in parent)
    {
      GameObject.Destroy(child.gameObject);
    }
  }

  // This is an event to check if a value is being pressed
  void OnEnable()
  {
    pressAction.performed += TouchPressed;
  }

  // This is an event to check is a value is not being pressed
  void OnDisable()
  {
    pressAction.performed -= TouchPressed;
  }

  // When this method is getting called, it will show a pop-up with the corresponding question
  void TouchPressed(InputAction.CallbackContext context)
  {
    Instantiate(question, question.transform.position, question.transform.rotation, parent);
  }

  public void DestroyObject()
  {
    Destroy(question);
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

