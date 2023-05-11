using UnityEngine;

[CreateAssetMenu(fileName = "ARItem", menuName = "ARItem", order = 1)]
public class ARItem : ScriptableObject
{
  public string name;
  public GameObject prefab;
  public Vector3 offsetPosition;
  public Vector3 offsetRotation;
  public Vector3 offsetScale;
  public bool recenter;
}