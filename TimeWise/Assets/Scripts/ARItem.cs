using UnityEngine;

[CreateAssetMenu(fileName = "ARItem", menuName = "ScriptableObjects/ARItem", order = 1)]
public class ARItem : ScriptableObject
{
  public string name;
  public GameObject prefab;
  public Vector3 offsetPosition = Vector3.zero;
  public Vector3 offsetRotation = Vector3.zero;
  public Vector3 offsetScale = new Vector3(1f,1f,1f);
  public bool recenter;
}