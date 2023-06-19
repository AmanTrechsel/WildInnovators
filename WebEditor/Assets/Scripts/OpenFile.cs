// Script from: https://www.youtube.com/watch?v=dcAfXEVcLMg

//MIT License
//Copyright (c) 2023 DA LAB (https://www.youtube.com/@DA-LAB)
//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:
//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.
//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.IO;
using System.Text;
using System.Collections;
using UnityEngine;
using SFB;
using UnityEngine.Networking;
using Dummiesman; //Load OBJ Model

public class OpenFile : MonoBehaviour
{
  [HideInInspector]
  public GameObject model; //Load OBJ Model

#if UNITY_WEBGL && !UNITY_EDITOR
  // WebGL
  [DllImport("__Internal")]
  private static extern void UploadFile(string gameObjectName, string methodName, string filter, bool multiple);

  public void OnClickOpen()
  {
    UploadFile(gameObject.name, "OnFileUpload", ".obj", false);
  }

  public void OnClickOpenTexture()
  {
    UploadFile(gameObject.name, "OnTextureUpload", ".png", false);
  }

  public void OnFileUpload(string url)
  {
    StartCoroutine(OutputRoutineOpenModel(url));
  }

  public void OnTextureUpload(string url)
  {
    StartCoroutine(OutputRoutineOpenTexture(url));
  }

#else
  // Standalone platforms & editor
  public void OnClickOpen()
  {
    string[] paths = StandaloneFileBrowser.OpenFilePanel("Open File", "", "obj", false);
    if (paths.Length > 0)
    {
      StartCoroutine(OutputRoutineOpenModel(new System.Uri(paths[0]).AbsoluteUri));
    }
  }

  public void OnClickOpenTexture()
  {
    string[] paths = StandaloneFileBrowser.OpenFilePanel("Open File", "", "png", false);
    if (paths.Length > 0)
    {
      StartCoroutine(OutputRoutineOpenTexture(new System.Uri(paths[0]).AbsoluteUri));
    }
  }
#endif

  private IEnumerator OutputRoutineOpenModel(string url)
  {
    UnityWebRequest www = UnityWebRequest.Get(url);
    yield return www.SendWebRequest();
    if (www.result != UnityWebRequest.Result.Success)
    {
      Debug.Log("WWW ERROR: " + www.error);
    }
    else
    {
      //Load OBJ Model
      byte[] modelBytes = Encoding.UTF8.GetBytes(www.downloadHandler.text);
      MemoryStream textStream = new MemoryStream(modelBytes);
      if (model != null)
      {
        Destroy(model);
      }
      model = new OBJLoader().Load(textStream);
      model.transform.localScale = new Vector3(-1, 1, 1); // set the position of parent model. Reverse X to show properly 

      // Add new model to ModelEditor
      ModelEditor.instance.AddModel(model, modelBytes);
    }
  }

  private IEnumerator OutputRoutineOpenTexture(string url)
  {
    UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
    yield return www.SendWebRequest();
    if (www.result != UnityWebRequest.Result.Success)
    {
      Debug.Log("WWW ERROR: " + www.error);
    }
    else
    {
      //Load Texture
      var texture = DownloadHandlerTexture.GetContent(www);
      texture.name = Path.GetFileNameWithoutExtension(url);

      ModelEditor.instance.AddUploadedImage(texture);
    }
  }
}