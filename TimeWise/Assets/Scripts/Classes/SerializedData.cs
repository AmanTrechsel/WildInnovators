using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Dummiesman; //Load OBJ Model

public class SerializedData
{
  // Name of the object
  public string displayName;

  // Position, rotation, and scale of the object
  public float[] position;
  public float[] rotation;
  public float[] scale;

  // Model
  public byte[] modelBytes;

  // Images
  public byte[] images;

  // Material data
  public float[] materialColors;
  public float[] materialMetallics;
  public float[] materialSmoothnesses;

  // Encyclopedia data
  public byte[] encyclopediaImage;
  public string encyclopediaDescription;

  // Serialize byte[][] to a byte array
  public static byte[] SerializeByteArray(byte[][] data)
  {
    BinaryFormatter formatter = new BinaryFormatter();
    using (MemoryStream stream = new MemoryStream())
    {
      formatter.Serialize(stream, data);
      return stream.ToArray();
    }
  }

  // Deserialize byte array to byte[][]
  public static byte[][] DeserializeByteArray(byte[] serializedData)
  {
    BinaryFormatter formatter = new BinaryFormatter();
    using (MemoryStream stream = new MemoryStream(serializedData))
    {
      return (byte[][])formatter.Deserialize(stream);
    }
  }

  // Constructs a GameObject from the serialized data
  public static GameObject Reconstruct(SerializedData serializedData)
  {
    // Create the GameObject
    GameObject newModel = new OBJLoader().Load(new MemoryStream(serializedData.modelBytes));
    newModel.name = serializedData.displayName;

    // Set the position, rotation, and scale
    newModel.transform.position = new Vector3(serializedData.position[0], serializedData.position[1], serializedData.position[2]);
    newModel.transform.rotation = Quaternion.Euler(serializedData.rotation[0], serializedData.rotation[1], serializedData.rotation[2]);
    newModel.transform.localScale = new Vector3(serializedData.scale[0], serializedData.scale[1], serializedData.scale[2]);

    // Create the textures
    List<Texture2D> images = new List<Texture2D>();
    Texture2D texture = null;
    byte[][] deserializedImages = DeserializeByteArray(serializedData.images);
    foreach (byte[] encodedImage in deserializedImages)
    {
      texture = new Texture2D(2, 2);
      texture.LoadImage(encodedImage);
      images.Add(texture);
    }

    // Create the materials
    List<Material> materials = new List<Material>();
    Shader defaultShader = Shader.Find("Standard");
    for (int i = 0; i < deserializedImages.Length; i++)
    {
      Material material = new Material(defaultShader);
      material.SetTexture("_MainTex", images[i]);
      material.color = new Color(serializedData.materialColors[i * 3], serializedData.materialColors[i * 3 + 1], serializedData.materialColors[i * 3 + 2]);
      material.SetFloat("_Metallic", serializedData.materialMetallics[i]);
      material.SetFloat("_Glossiness", serializedData.materialSmoothnesses[i]);
      materials.Add(material);
    }

    // Set the materials
    newModel.GetComponentInChildren<MeshRenderer>().materials = materials.ToArray();

    // Set the encyclopedia image
    Texture2D encyclopediaImage = new Texture2D(2, 2);
    encyclopediaImage.LoadImage(serializedData.encyclopediaImage);

    // Set the encyclopedia description
    string encyclopediaDescription = serializedData.encyclopediaDescription;

    // Return the GameObject
    return newModel;
  }
}