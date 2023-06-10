using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerializedData
{
  public string displayName;

  public float[] position;
  public float[] rotation;
  public float[] scale;

  public float[] meshVertices;
  public float[] meshUVs;
  public int[] meshTriangles;
  public int[] images;
  public int[] imageInfos;

  public float[] materialColors;
  public float[] materialMetallics;
  public float[] materialSmoothnesses;

  public static GameObject Reconstruct(SerializedData serializedData)
  {
    GameObject newModel = new GameObject(serializedData.displayName);
    newModel.transform.position = new Vector3(serializedData.position[0], serializedData.position[1], serializedData.position[2]);
    newModel.transform.rotation = Quaternion.Euler(serializedData.rotation[0], serializedData.rotation[1], serializedData.rotation[2]);
    newModel.transform.localScale = new Vector3(serializedData.scale[0], serializedData.scale[1], serializedData.scale[2]);

    MeshFilter meshFilter = newModel.AddComponent<MeshFilter>();
    MeshRenderer meshRenderer = newModel.AddComponent<MeshRenderer>();

    Mesh mesh = new Mesh();
    Vector3[] meshVertices = new Vector3[serializedData.meshVertices.Length / 3];
    for (int i = 0; i < serializedData.meshVertices.Length; i += 3)
    {
      meshVertices[i / 3] = new Vector3(serializedData.meshVertices[i], serializedData.meshVertices[i + 1], serializedData.meshVertices[i + 2]);
    }
    Vector2[] meshUV = new Vector2[serializedData.meshUVs.Length / 2];
    for (int i = 0; i < serializedData.meshUVs.Length; i += 2)
    {
      meshUV[i / 2] = new Vector2(serializedData.meshUVs[i], serializedData.meshUVs[i + 1]);
    }
    mesh.vertices = meshVertices;
    mesh.uv = meshUV;
    mesh.triangles = serializedData.meshTriangles;
    mesh.RecalculateNormals();
    meshFilter.mesh = UnityEngine.Object.Instantiate(mesh);

    List<Texture2D> images = new List<Texture2D>();
    Texture2D texture = null;
    for (int i = 0; i < serializedData.images.Length; i++)
    {
      texture = new Texture2D(2, 2);
      texture.LoadImage(BitConverter.GetBytes(serializedData.images[i]));
      images.Add(texture);
    }

    List<Material> materials = new List<Material>();
    Shader defaultShader = Shader.Find("Standard");
    for (int i = 0; i < serializedData.images.Length; i++)
    {
      Material material = new Material(defaultShader);
      material.SetTexture("_MainTex", images[i]);
      material.color = new Color(serializedData.materialColors[i * 3], serializedData.materialColors[i * 3 + 1], serializedData.materialColors[i * 3 + 2]);
      material.SetFloat("_Metallic", serializedData.materialMetallics[i]);
      material.SetFloat("_Glossiness", serializedData.materialSmoothnesses[i]);
      materials.Add(material);
    }

    meshRenderer.materials = materials.ToArray();

    return newModel;
  }

  public static SerializedData Create(string displayName, Vector3 position, Vector3 rotation, Vector3 scale, Vector3[] meshVertices, Vector2[] meshUVs,
                               int[] meshTriangles, Texture2D[] images, Color[] materialColors, float[] materialMetallics, float[] materialSmoothnesses)
  {
    SerializedData newData = new SerializedData();

    newData.displayName = displayName;

    newData.position = new float[] { position.x, position.y, position.z };
    newData.rotation = new float[] { rotation.x, rotation.y, rotation.z };
    newData.scale = new float[] { scale.x, scale.y, scale.z };

    List<float> listMeshVertices = new List<float>();
    foreach (Vector3 meshVertex in meshVertices)
    {
      listMeshVertices.Add(meshVertex.x);
      listMeshVertices.Add(meshVertex.y);
      listMeshVertices.Add(meshVertex.z);
    }
    newData.meshVertices = listMeshVertices.ToArray();

    List<float> listMeshUVs = new List<float>();
    foreach (Vector2 meshUV in meshUVs)
    {
      listMeshUVs.Add(meshUV.x);
      listMeshUVs.Add(meshUV.y);
    }
    newData.meshUVs = listMeshUVs.ToArray();

    newData.meshTriangles = meshTriangles;

    List<int> listImages = new List<int>();
    List<int> listImageInfos = new List<int>();
    listImageInfos.Add(images.Length);
    foreach (Texture2D image in images)
    {
      byte[] encodedImage = ImageConversion.EncodeToPNG(image);
      listImageInfos.Add(encodedImage.Length);
      listImages.Add(BitConverter.ToInt32(encodedImage));
    }
    newData.imageInfos = listImageInfos.ToArray();
    newData.images = listImages.ToArray();

    List<float> listMaterialColors = new List<float>();
    foreach (Color materialColor in materialColors)
    {
      listMaterialColors.Add(Mathf.Round(materialColor.r * 100) / 100f);
      listMaterialColors.Add(Mathf.Round(materialColor.g * 100) / 100f);
      listMaterialColors.Add(Mathf.Round(materialColor.b * 100) / 100f);
      listMaterialColors.Add(Mathf.Round(materialColor.a * 100) / 100f);
    }
    newData.materialColors = listMaterialColors.ToArray();

    List<float> listMaterialMetallics = new List<float>( materialMetallics );
    newData.materialMetallics = listMaterialMetallics.ToArray();

    List<float> listMaterialSmoothnesses = new List<float>( materialSmoothnesses );
    newData.materialSmoothnesses = listMaterialSmoothnesses.ToArray();

    return newData;
  }
}
