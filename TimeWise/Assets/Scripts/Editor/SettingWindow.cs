using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class SettingEditorWindow : EditorWindow
{
  private List<Setting> settingObjects = new List<Setting>();
  private Setting selectedSetting;
  private int selectedSettingIndex;
  private Vector2 scrollPos;

  [MenuItem("Custom/Setting Editor")]
  public static void ShowWindow()
  {
    EditorWindow.GetWindow<SettingEditorWindow>("Setting Editor");
  }

  private void OnEnable()
  {
    LoadSettingObjects();
  }

  private void OnGUI()
  {
    GUILayout.BeginHorizontal();
    GUILayout.BeginVertical(GUILayout.Width(200));
    GUILayout.Label("Setting List", EditorStyles.boldLabel);
    scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.ExpandWidth(true));
    for (int i = 0; i < settingObjects.Count; i++)
    {
      if (GUILayout.Toggle(i == selectedSettingIndex, settingObjects[i].name, "Button"))
      {
        if (selectedSettingIndex != i)
        {
          selectedSettingIndex = i;
          selectedSetting = settingObjects[i];
          GUI.FocusControl(null);
        }
      }
    }
    EditorGUILayout.EndScrollView();
    GUILayout.Space(20);
    GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(5));
    GUILayout.BeginHorizontal(GUILayout.Width(200));
    if (GUILayout.Button("+"))
    {
      CreateNewSetting();
      GUI.FocusControl(null);
    }

    if (selectedSetting != null && GUILayout.Button("-"))
    {
      DeleteSelectedSetting();
      GUI.FocusControl(null);
    }

    GUILayout.EndHorizontal();
    GUILayout.EndVertical();
    GUILayout.Box("", GUILayout.ExpandHeight(true), GUILayout.Width(5));
    if (selectedSetting != null)
    {
      GUILayout.BeginVertical();
      GUILayout.Label("Selected Setting", EditorStyles.boldLabel);
      string newFilename = EditorGUILayout.TextField("Filename", selectedSetting.name);
      if (newFilename != selectedSetting.name)
      {
        string newPath = AssetDatabase.GetAssetPath(selectedSetting);
        string newFullPath = newPath.Replace(selectedSetting.name + ".asset", newFilename + ".asset");
        AssetDatabase.RenameAsset(newPath, newFilename);
        AssetDatabase.SaveAssets();
        selectedSetting.name = newFilename;
      }

      Editor editor = Editor.CreateEditor(selectedSetting);
      editor.DrawDefaultInspector();
      GUILayout.Space(20);
      if (selectedSetting != null && GUILayout.Button("Highlight Selected Setting"))
      {
        EditorGUIUtility.PingObject(selectedSetting);
      }

      GUILayout.EndVertical();
    }
    GUILayout.EndHorizontal();
  }

    private void LoadSettingObjects()
    {
      settingObjects.Clear();
      string[] assetGUIDs = AssetDatabase.FindAssets("t:Setting", new[] { "Assets/Resources/Settings" });
      foreach (string assetGUID in assetGUIDs)
      {
        string assetPath = AssetDatabase.GUIDToAssetPath(assetGUID);
        Setting setting = AssetDatabase.LoadAssetAtPath<Setting>(assetPath);
        if (setting != null)
        {
          settingObjects.Add(setting);
        }
      }

      if (settingObjects.Count > 0)
      {
        selectedSettingIndex = 0;
        selectedSetting = settingObjects[0];
      }
    }

    private void CreateNewSetting()
    {
      string folderPath = "Assets/Resources/Settings";
      string settingName = "New Setting";
      string settingPath = folderPath + "/" + settingName + ".asset";
      int settingIndex = 1;
      while (AssetDatabase.LoadAssetAtPath<Setting>(settingPath) != null)
      {
        settingIndex++;
        settingName = "New Setting (" + settingIndex + ")";
        settingPath = folderPath + "/" + settingName + ".asset";
      }
      
      Setting newSetting = ScriptableObject.CreateInstance<Setting>();
      AssetDatabase.CreateAsset(newSetting, settingPath);
      AssetDatabase.SaveAssets();
      LoadSettingObjects();
      int index = settingObjects.FindIndex(setting => setting == newSetting);
      selectedSettingIndex = index;
      selectedSetting = newSetting;
    }

    private void DeleteSelectedSetting()
    {
      if (selectedSetting != null)
      {
        string assetPath = AssetDatabase.GetAssetPath(selectedSetting);
        AssetDatabase.DeleteAsset(assetPath);
        settingObjects.Remove(selectedSetting);
        selectedSettingIndex = Mathf.Clamp(selectedSettingIndex, 0, settingObjects.Count - 1);
        selectedSetting = (settingObjects.Count > 0) ? settingObjects[selectedSettingIndex] : null;
      }
    }

    private void SaveSelectedSetting()
    {
      if (selectedSetting != null)
      {
        EditorUtility.SetDirty(selectedSetting);
        AssetDatabase.SaveAssets();
      }
    }
    
}
