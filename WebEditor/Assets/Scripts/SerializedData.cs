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
  public float[] images;

  public float[] materialColors;
  public float[] materialMetallics;
  public float[] materialSmoothnesses;

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

    List<float> listImages = new List<float>();
    foreach (Texture2D image in images)
    {
      for (int x = 0; x < image.width; x++)
      {
        for (int y = 0; y < image.height; y++)
        {
          Color pixelColor = image.GetPixel(x,y);
          listImages.Add(x);
          listImages.Add(y);
          listImages.Add(pixelColor.r);
          listImages.Add(pixelColor.g);
          listImages.Add(pixelColor.b);
          listImages.Add(pixelColor.a);
        }
      }
    }
    newData.images = listImages.ToArray();

    List<float> listMaterialColors = new List<float>();
    foreach (Color materialColor in materialColors)
    {
      listMaterialColors.Add(materialColor.r);
      listMaterialColors.Add(materialColor.g);
      listMaterialColors.Add(materialColor.b);
      listMaterialColors.Add(materialColor.a);
    }
    newData.materialColors = listMaterialColors.ToArray();

    List<float> listMaterialMetallics = new List<float>( materialMetallics );
    newData.materialMetallics = listMaterialMetallics.ToArray();

    List<float> listMaterialSmoothnesses = new List<float>( materialSmoothnesses );
    newData.materialSmoothnesses = listMaterialSmoothnesses.ToArray();

    return newData;
  }
}
