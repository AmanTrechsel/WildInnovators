using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ModelUploader : MonoBehaviour
{
  public void UploadModel()
  {
    // Open file selection dialog
    string[] fileTypes = { "fbx", "obj" }; // Supported file types
    string fileURL = "file://" + Application.persistentDataPath; // File URL

    NativeFilePicker.Permission result = NativeFilePicker.PickFile((pickedFiles) =>
    {
      if (pickedFiles.Length > 0)
      {
        string filePath = pickedFiles;

        // Start file upload
        StartCoroutine(UploadFile(filePath));
      }
    }, fileTypes);
  }

  IEnumerator UploadFile(string filePath)
  {
    WWWForm form = new WWWForm();
    byte[] fileData = System.IO.File.ReadAllBytes(filePath);
    form.AddBinaryData("file", fileData, Path.GetFileName(filePath));

    using (UnityWebRequest www = UnityWebRequest.Post("https://timewise.serverict.nl/inlog.php", form))
    {
      yield return www;

      if (string.IsNullOrEmpty(www.error))
      {
        string uploadedPath = www.downloadHandler.text;
        // Handle the response from the server
        Debug.Log("File upload successful! " + uploadedPath);
        // Instantiate and display the model
        InstantiateModel(uploadedPath);
      }
      else
      {
        Debug.LogError("File upload error: " + www.error);
      }
    }
  }

  void InstantiateModel(string modelPath)
  {
    // Load the model into Unity
    GameObject model = Instantiate(Resources.Load<GameObject>(modelPath));
  }
}