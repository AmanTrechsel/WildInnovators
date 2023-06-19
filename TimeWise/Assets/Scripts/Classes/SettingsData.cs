using UnityEngine;

[System.Serializable]
public class SettingsData
{
  // Whether the user has given permission to use their camera
  public bool permission;
  // Which language the user has selected
  public int languageIndex;
  // The location of the images
  public string location;
  // Whether the user has clicked an object before
  public bool hasClicked;
}
