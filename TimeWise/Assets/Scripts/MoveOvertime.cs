using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOvertime : MonoBehaviour
{
  // The speed to move at
  [SerializeField]
  private Vector3 moveSpeed;
  // The speed to rotate at
  [SerializeField]
  private Vector3 rotationSpeed;

  // Update is called once per frame
  private void Update()
  {
    // Move and rotate the object over time
    transform.Translate(moveSpeed * Time.deltaTime);
    transform.Rotate(rotationSpeed * Time.deltaTime);
  }
}
