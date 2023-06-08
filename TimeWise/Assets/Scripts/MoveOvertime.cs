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
  // Optional check to randomize the movements
  [SerializeField]
  private bool randomizeMovement = false;

  // Update is called once per frame
  private void Update()
  {
    // Move and rotate the object over time
    if(randomizeMovement == false)
    {
      MoveObject(moveSpeed, rotationSpeed);
    }
    else if(randomizeMovement == true)
    {
      MoveObject(moveSpeed, rotationSpeed);
      StartCoroutine(RandomizeValues());
    }
  }

  // Method that makes the object move and rotate
  public void MoveObject(Vector3 moveSpeed, Vector3 rotationSpeed)
  {
    transform.Translate(moveSpeed * Time.deltaTime);
    transform.Rotate(rotationSpeed * Time.deltaTime);
  }
  
  // For the coroutine, to randomize moveSpeed and rotationSpeed values
  IEnumerator RandomizeValues()
  {
    yield return new WaitForSeconds(Random.Range(10.0f, 50.0f));

    int randomValue = Random.Range(0, 1);

    if(randomValue == 0)
    {
      moveSpeed.x += Random.Range(-10.0f, 10.0f);
      moveSpeed.z += Random.Range(-10.0f, 10.0f);
    }
    else if(randomValue == 1)
    {
      rotationSpeed.y += Random.Range(-50.0f, 50.0f);
    }
  }
}
