using UnityEngine;

[CreateAssetMenu(fileName = "ARItem", menuName = "ScriptableObjects/ARItem", order = 1)]
public class ARItem : ScriptableObject
{
  // The name of the AR Item
  public string name;
  // The prefab for the AR Item
  public GameObject prefab;
  // The offset position for the AR Item
  public Vector3 offsetPosition = Vector3.zero;
  // The offset rotation for the AR Item
  public Vector3 offsetRotation = Vector3.zero;
  // The offset scale for the AR Item
  public Vector3 offsetScale = new Vector3(1f, 1f, 1f);
}