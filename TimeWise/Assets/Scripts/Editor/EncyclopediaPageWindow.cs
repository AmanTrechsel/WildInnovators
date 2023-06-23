using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class EncyclopediaPageEditorWindow : EditorWindow
{
  private List<EncyclopediaPage> encyclopediaPageObjects = new List<EncyclopediaPage>();
  private EncyclopediaPage selectedEncyclopediaPage;
  private int selectedEncyclopediaPageIndex;
  private Vector2 scrollPos;

  [MenuItem("Custom/EncyclopediaPage Editor")]
  public static void ShowWindow()
  {
    EditorWindow.GetWindow<EncyclopediaPageEditorWindow>("EncyclopediaPage Editor");
  }

  private void OnEnable()
  {
    LoadEncyclopediaPageObjects();
  }

  private void OnGUI()
  {
    GUILayout.BeginHorizontal();
    GUILayout.BeginVertical(GUILayout.Width(200));
    GUILayout.Label("EncyclopediaPage List", EditorStyles.boldLabel);
    scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.ExpandWidth(true));
    for (int i = 0; i < encyclopediaPageObjects.Count; i++)
    {
      if (GUILayout.Toggle(i == selectedEncyclopediaPageIndex, encyclopediaPageObjects[i].name, "Button"))
      {
        if (selectedEncyclopediaPageIndex != i)
        {
          selectedEncyclopediaPageIndex = i;
          selectedEncyclopediaPage = encyclopediaPageObjects[i];
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
      CreateNewEncyclopediaPage();
      GUI.FocusControl(null);
    }

    if (selectedEncyclopediaPage != null && GUILayout.Button("-"))
    {
      DeleteSelectedEncyclopediaPage();
      GUI.FocusControl(null);
    }

    GUILayout.EndHorizontal();
    GUILayout.EndVertical();
    GUILayout.Box("", GUILayout.ExpandHeight(true), GUILayout.Width(5));
    if (selectedEncyclopediaPage != null)
    {
      GUILayout.BeginVertical();
      GUILayout.Label("Selected EncyclopediaPage", EditorStyles.boldLabel);
      string newFilename = EditorGUILayout.TextField("Filename", selectedEncyclopediaPage.name);
      if (newFilename != selectedEncyclopediaPage.name)
      {
        string newPath = AssetDatabase.GetAssetPath(selectedEncyclopediaPage);
        string newFullPath = newPath.Replace(selectedEncyclopediaPage.name + ".asset", newFilename + ".asset");
        AssetDatabase.RenameAsset(newPath, newFilename);
        AssetDatabase.SaveAssets();
        selectedEncyclopediaPage.name = newFilename;
      }

      Editor editor = Editor.CreateEditor(selectedEncyclopediaPage);
      editor.DrawDefaultInspector();
      GUILayout.Space(20);
      if (selectedEncyclopediaPage != null && GUILayout.Button("Highlight Selected EncyclopediaPage"))
      {
        EditorGUIUtility.PingObject(selectedEncyclopediaPage);
      }

      GUILayout.EndVertical();
    }
    GUILayout.EndHorizontal();
  }

    private void LoadEncyclopediaPageObjects()
    {
      encyclopediaPageObjects.Clear();
      string[] assetGUIDs = AssetDatabase.FindAssets("t:EncyclopediaPage", new[] { "Assets/Resources/EncyclopediaPages" });
      foreach (string assetGUID in assetGUIDs)
      {
        string assetPath = AssetDatabase.GUIDToAssetPath(assetGUID);
        EncyclopediaPage encyclopediaPage = AssetDatabase.LoadAssetAtPath<EncyclopediaPage>(assetPath);
        if (encyclopediaPage != null)
        {
          encyclopediaPageObjects.Add(encyclopediaPage);
        }
      }

      if (encyclopediaPageObjects.Count > 0)
      {
        selectedEncyclopediaPageIndex = 0;
        selectedEncyclopediaPage = encyclopediaPageObjects[0];
      }
    }

    private void CreateNewEncyclopediaPage()
    {
      string folderPath = "Assets/Resources/EncyclopediaPages";
      string encyclopediaPageName = "New EncyclopediaPage";
      string encyclopediaPagePath = folderPath + "/" + encyclopediaPageName + ".asset";
      int encyclopediaPageIndex = 1;
      while (AssetDatabase.LoadAssetAtPath<EncyclopediaPage>(encyclopediaPagePath) != null)
      {
        encyclopediaPageIndex++;
        encyclopediaPageName = "New EncyclopediaPage (" + encyclopediaPageIndex + ")";
        encyclopediaPagePath = folderPath + "/" + encyclopediaPageName + ".asset";
      }
      
      EncyclopediaPage newEncyclopediaPage = ScriptableObject.CreateInstance<EncyclopediaPage>();
      AssetDatabase.CreateAsset(newEncyclopediaPage, encyclopediaPagePath);
      AssetDatabase.SaveAssets();
      LoadEncyclopediaPageObjects();
      int index = encyclopediaPageObjects.FindIndex(encyclopediaPage => encyclopediaPage == newEncyclopediaPage);
      selectedEncyclopediaPageIndex = index;
      selectedEncyclopediaPage = newEncyclopediaPage;
    }

    private void DeleteSelectedEncyclopediaPage()
    {
      if (selectedEncyclopediaPage != null)
      {
        string assetPath = AssetDatabase.GetAssetPath(selectedEncyclopediaPage);
        AssetDatabase.DeleteAsset(assetPath);
        encyclopediaPageObjects.Remove(selectedEncyclopediaPage);
        selectedEncyclopediaPageIndex = Mathf.Clamp(selectedEncyclopediaPageIndex, 0, encyclopediaPageObjects.Count - 1);
        selectedEncyclopediaPage = (encyclopediaPageObjects.Count > 0) ? encyclopediaPageObjects[selectedEncyclopediaPageIndex] : null;
      }
    }

    private void SaveSelectedEncyclopediaPage()
    {
      if (selectedEncyclopediaPage != null)
      {
        EditorUtility.SetDirty(selectedEncyclopediaPage);
        AssetDatabase.SaveAssets();
      }
    }
}