using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class CodeInputManager : MonoBehaviour
{
  [SerializeField]
  private GameObject invalidCode;
  [SerializeField]
  private TMP_InputField codeInput;

  // Called when the code is submitted
  public void SubmitCode()
  {
    // Hide the invalid code message
    invalidCode.SetActive(false);

    // Start the web request
    StartCoroutine(HandleWebRequest());
  }

  // Handles the web request response
  private IEnumerator HandleWebRequest()
  {
    // Create a new web request
    UnityWebRequest webRequest = UnityWebRequest.Get("https://timewise.serverict.nl/get_lesson.php?code=" + codeInput.text);

    // Send the web request
    yield return webRequest.SendWebRequest();

    // Wait until the web request is done
    while (!webRequest.isDone) { yield return null; }

    // Check if the web request was successful
    if (webRequest.result == UnityWebRequest.Result.Success)
    {
      // Load the lesson data
      string json = webRequest.downloadHandler.text;

      // Deserialize the lesson data
      SerializedData lessonData = JsonUtility.FromJson<SerializedData>(json);

      // Reconstruct the model
      GameObject newModel = SerializedData.Reconstruct(lessonData);

      // Go back to the course selection menu
      GoBack();
    }
    else
    {
      // Show the invalid code message
      invalidCode.SetActive(true);
    }
  }

  // Sends back to the course selection menu
  public void GoBack()
  {
    AppManager.Instance.LoadScene("CourseSelect");
  }
  
  // Opens the settings menu
  public void GoToSettings()
  {
    AppManager.Instance.GoToSettings();
  }
}
