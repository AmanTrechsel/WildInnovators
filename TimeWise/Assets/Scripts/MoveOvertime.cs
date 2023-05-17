using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOvertime : MonoBehaviour
{
  [SerializeField]
  private Vector3 moveSpeed;
  [SerializeField]
  private Vector3 rotationSpeed;

  private void Update()
  {
    transform.Translate(moveSpeed * Time.deltaTime);
    transform.Rotate(rotationSpeed * Time.deltaTime);
  }
}
