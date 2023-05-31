using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

// This script acts as an in engine editor for ARItems
public class ARItemEditorWindow : EditorWindow
{
  // A list containing all ARItems
  private List<ARItem> arItemObjects = new List<ARItem>();
  // Currently selected ARItem
  private ARItem selectedARItem;
  // The index for the selected ARItem
  private int selectedARItemIndex;
  // A vector2 containing the current position of scrolling
  private Vector2 scrollPos;

  // Method for showing the window in the custom tab
  [MenuItem("Custom/ARItem Editor")]
  public static void ShowWindow()
  {
    EditorWindow.GetWindow<ARItemEditorWindow>("ARItem Editor");
  }

  private void OnEnable()
  {
    LoadARItemObjects();
  }

  private void OnGUI()
  {
    GUILayout.BeginHorizontal();
    GUILayout.BeginVertical(GUILayout.Width(200));
    GUILayout.Label("ARItem List", EditorStyles.boldLabel);
    scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.ExpandWidth(true));
    for (int i = 0; i < arItemObjects.Count; i++)
    {
      if (GUILayout.Toggle(i == selectedARItemIndex, arItemObjects[i].name, "Button"))
      {
        if (selectedARItemIndex != i)
        {
          selectedARItemIndex = i;
          selectedARItem = arItemObjects[i];
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
      CreateNewARItem();
      GUI.FocusControl(null);
    }
    if (selectedARItem != null && GUILayout.Button("-"))
    {
      DeleteSelectedARItem();
      GUI.FocusControl(null);
    }
    GUILayout.EndHorizontal();
    GUILayout.EndVertical();
    GUILayout.Box("", GUILayout.ExpandHeight(true), GUILayout.Width(5));
    if (selectedARItem != null)
    {
      GUILayout.BeginVertical();
      GUILayout.Label("Selected ARItem", EditorStyles.boldLabel);
      string newFilename = EditorGUILayout.TextField("Filename", selectedARItem.name);
      if (newFilename != selectedARItem.name)
      {
        string newPath = AssetDatabase.GetAssetPath(selectedARItem);
        string newFullPath = newPath.Replace(selectedARItem.name + ".asset", newFilename + ".asset");
        AssetDatabase.RenameAsset(newPath, newFilename);
        AssetDatabase.SaveAssets();
        selectedARItem.name = newFilename;
      }
      Editor editor = Editor.CreateEditor(selectedARItem);
      editor.DrawDefaultInspector();
      GUILayout.Space(20);
      if (selectedARItem != null && GUILayout.Button("Highlight Selected ARItem"))
      {
        EditorGUIUtility.PingObject(selectedARItem);
      }
      GUILayout.EndVertical();
    }
    GUILayout.EndHorizontal();
  }

    private void LoadARItemObjects()
    {
      arItemObjects.Clear();
      string[] assetGUIDs = AssetDatabase.FindAssets("t:ARItem", new[] { "Assets/Resources/ARItems" });
      foreach (string assetGUID in assetGUIDs)
      {
        string assetPath = AssetDatabase.GUIDToAssetPath(assetGUID);
        ARItem arItem = AssetDatabase.LoadAssetAtPath<ARItem>(assetPath);
        if (arItem != null)
        {
          arItemObjects.Add(arItem);
        }
      }
      if (arItemObjects.Count > 0)
      {
        selectedARItemIndex = 0;
        selectedARItem = arItemObjects[0];
      }
    }

    private void CreateNewARItem()
    {
      string folderPath = "Assets/Resources/ARItems";
      string arItemName = "New ARItem";
      string arItemPath = folderPath + "/" + arItemName + ".asset";
      int arItemIndex = 1;
      while (AssetDatabase.LoadAssetAtPath<ARItem>(arItemPath) != null)
      {
        arItemIndex++;
        arItemName = "New ARItem (" + arItemIndex + ")";
        arItemPath = folderPath + "/" + arItemName + ".asset";
      }
      ARItem newARItem = ScriptableObject.CreateInstance<ARItem>();
      AssetDatabase.CreateAsset(newARItem, arItemPath);
      AssetDatabase.SaveAssets();
      LoadARItemObjects();
      int index = arItemObjects.FindIndex(arItem => arItem == newARItem);
      selectedARItemIndex = index;
      selectedARItem = newARItem;
    }

    private void DeleteSelectedARItem()
    {
      if (selectedARItem != null)
      {
        string assetPath = AssetDatabase.GetAssetPath(selectedARItem);
        AssetDatabase.DeleteAsset(assetPath);
        arItemObjects.Remove(selectedARItem);
        selectedARItemIndex = Mathf.Clamp(selectedARItemIndex, 0, arItemObjects.Count - 1);
        selectedARItem = (arItemObjects.Count > 0) ? arItemObjects[selectedARItemIndex] : null;
      }
    }

    private void SaveSelectedARItem()
    {
      if (selectedARItem != null)
      {
        EditorUtility.SetDirty(selectedARItem);
        AssetDatabase.SaveAssets();
      }
    }
}