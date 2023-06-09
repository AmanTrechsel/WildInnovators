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

  // Load all ARItems when the window opens
  private void OnEnable()
  {
    LoadARItemObjects();
  }

  // Ran when interacting with the window
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

  // Loads all objects from the resources folder
  private void LoadARItemObjects()
  {
    // Delete existing list
    arItemObjects.Clear();
    // Find all assets
    string[] assetGUIDs = AssetDatabase.FindAssets("t:ARItem", new[] { "Assets/Resources/ARItems" });
    // Iterate through all the assets
    foreach (string assetGUID in assetGUIDs)
    {
      // Find path based on the asset
      string assetPath = AssetDatabase.GUIDToAssetPath(assetGUID);
      // Get the item from the asset database
      ARItem arItem = AssetDatabase.LoadAssetAtPath<ARItem>(assetPath);
      // Check if the item exists
      if (arItem != null)
      {
        // Add item to the list
        arItemObjects.Add(arItem);
      }
    }
    
    // Check if there are any objects
    if (arItemObjects.Count > 0)
    {
      // Select the first index
      selectedARItemIndex = 0;
      selectedARItem = arItemObjects[0];
    }
  }

  // Create a new ARItem
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

  // Delete the selected ARItem
  private void DeleteSelectedARItem()
  {
    // Check if there is a selected ARItem
    if (selectedARItem != null)
    {
      // Delete the selected ARItem
      string assetPath = AssetDatabase.GetAssetPath(selectedARItem);
      AssetDatabase.DeleteAsset(assetPath);
      arItemObjects.Remove(selectedARItem);
      selectedARItemIndex = Mathf.Clamp(selectedARItemIndex, 0, arItemObjects.Count - 1);
      selectedARItem = (arItemObjects.Count > 0) ? arItemObjects[selectedARItemIndex] : null;
    }
  }

  // Save the selected ARItem
  private void SaveSelectedARItem()
  {
    // Check if there is a selected ARItem
    if (selectedARItem != null)
    {
      // Mark the selected ARItem as dirty
      EditorUtility.SetDirty(selectedARItem);
      // Save the selected ARItem
      AssetDatabase.SaveAssets();
    }
  }
}