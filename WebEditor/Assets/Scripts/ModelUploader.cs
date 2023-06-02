using System.IO;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Firebase.Storage;
using Firebase.Extensions;
using System.Threading.Tasks;

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

        // Upload file to FireBase
        UploadFile(filePath);

        // Create model
        NativeFilePicker.ExportFile(filePath);
        InstantiateModel(filePath);
      }
    }, fileTypes);
  }

  void UploadFile(string path)
  {
    FirebaseStorage storage = FirebaseStorage.DefaultInstance;

    // Create a root reference
    StorageReference storageRef = storage.RootReference;

    // Create a reference to the file you want to upload
    StorageReference riversRef = storageRef.Child("images/rivers.jpg");

    // Upload the file to the path "images/rivers.jpg"
    riversRef.PutFileAsync(path).ContinueWith((Task<StorageMetadata> task) =>
    {
      if (task.IsFaulted || task.IsCanceled)
      {
        Debug.Log(task.Exception.ToString());
        // Uh-oh, an error occurred!
      }
      else
      {
        // Metadata contains file metadata such as size, content-type, and download URL.
        StorageMetadata metadata = task.Result;
        string md5Hash = metadata.Md5Hash;
        Debug.Log("Finished uploading...");
        Debug.Log("md5 hash = " + md5Hash);
      }
    });
  }

  void DownloadFile(string filename)
  {
    FirebaseStorage storage = FirebaseStorage.DefaultInstance;

    // Create a reference with an initial file path and name
    StorageReference pathReference = storage.GetReference("images/stars.jpg");

    // Download in memory with a maximum allowed size of 1MB (1 * 1024 * 1024 bytes)
    const long maxAllowedSize = 1 * 1024 * 1024;
    pathReference.GetBytesAsync(maxAllowedSize).ContinueWithOnMainThread(task =>
    {
      if (task.IsFaulted || task.IsCanceled)
      {
        Debug.LogException(task.Exception);
        // Uh-oh, an error occurred!
      }
      else
      {
        byte[] fileContents = task.Result;
        Debug.Log("Finished downloading!");
      }
    });
  }

  void InstantiateModel(string modelPath)
  {
    Debug.Log(modelPath);
    // Load the model into Unity
    //GameObject model = (GameObject)Instantiate(File.Open(modelPath), Vector3.zero, Quaternion.identity);
  }
}