using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Animal", menuName = "Animal", order = 1)]
public class Animal : ScriptableObject
{
  // Possible subjects the animal can be a part of
  public enum Subject : int { Biology = 0, Geography = 1, History = 2 };

  // Display name of the animal
  public string name;

  // The prefab that will be shown
  public GameObject prefab;
  
  // Display image for the animal
  public Sprite displayImage;

  // Period in which the animal lived (x = min, y = max)
  public Vector2 period;

  // Subject of the animal
  public Subject subject;
}
//