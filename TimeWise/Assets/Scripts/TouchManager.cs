using UnityEngine;
using UnityEngine.InputSystem;

public class TouchManager : MonoBehaviour
{
  [SerializeField]
  private GameObject question;

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

    question.SetActive(true);
  }
}

