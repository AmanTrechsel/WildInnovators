using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class SubjectEditorWindow : EditorWindow
{
  private List<Subject> subjectObjects = new List<Subject>();
  private Subject selectedSubject;
  private int selectedSubjectIndex;
  private Vector2 scrollPos;

  [MenuItem("Custom/Subject Editor")]
  public static void ShowWindow()
  {
    EditorWindow.GetWindow<SubjectEditorWindow>("Subject Editor");
  }

  private void OnEnable()
  {
    LoadSubjectObjects();
  }

  private void OnGUI()
  {
    GUILayout.BeginHorizontal();
    GUILayout.BeginVertical(GUILayout.Width(200));
    GUILayout.Label("Subject List", EditorStyles.boldLabel);
    scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.ExpandWidth(true));
    for (int i = 0; i < subjectObjects.Count; i++)
    {
      if (GUILayout.Toggle(i == selectedSubjectIndex, subjectObjects[i].name, "Button"))
      {
        if (selectedSubjectIndex != i)
        {
          selectedSubjectIndex = i;
          selectedSubject = subjectObjects[i];
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
      CreateNewSubject();
      GUI.FocusControl(null);
    }

    if (selectedSubject != null && GUILayout.Button("-"))
    {
      DeleteSelectedSubject();
      GUI.FocusControl(null);
    }

    GUILayout.EndHorizontal();
    GUILayout.EndVertical();
    GUILayout.Box("", GUILayout.ExpandHeight(true), GUILayout.Width(5));
    if (selectedSubject != null)
    {
      GUILayout.BeginVertical();
      GUILayout.Label("Selected Subject", EditorStyles.boldLabel);
      string newFilename = EditorGUILayout.TextField("Filename", selectedSubject.name);
      if (newFilename != selectedSubject.name)
      {
        string newPath = AssetDatabase.GetAssetPath(selectedSubject);
        string newFullPath = newPath.Replace(selectedSubject.name + ".asset", newFilename + ".asset");
        AssetDatabase.RenameAsset(newPath, newFilename);
        AssetDatabase.SaveAssets();
        selectedSubject.name = newFilename;
      }

      Editor editor = Editor.CreateEditor(selectedSubject);
      editor.DrawDefaultInspector();
      GUILayout.Space(20);
      if (selectedSubject != null && GUILayout.Button("Highlight Selected Subject"))
      {
        EditorGUIUtility.PingObject(selectedSubject);
      }

      GUILayout.EndVertical();
    }
    GUILayout.EndHorizontal();
  }

    private void LoadSubjectObjects()
    {
      subjectObjects.Clear();
      string[] assetGUIDs = AssetDatabase.FindAssets("t:Subject", new[] { "Assets/Resources/Subjects" });
      foreach (string assetGUID in assetGUIDs)
      {
        string assetPath = AssetDatabase.GUIDToAssetPath(assetGUID);
        Subject subject = AssetDatabase.LoadAssetAtPath<Subject>(assetPath);
        if (subject != null)
        {
          subjectObjects.Add(subject);
        }
      }

      if (subjectObjects.Count > 0)
      {
        selectedSubjectIndex = 0;
        selectedSubject = subjectObjects[0];
      }
    }

    private void CreateNewSubject()
    {
      string folderPath = "Assets/Resources/Subjects";
      string subjectName = "New Subject";
      string subjectPath = folderPath + "/" + subjectName + ".asset";
      int subjectIndex = 1;
      while (AssetDatabase.LoadAssetAtPath<Subject>(subjectPath) != null)
      {
        subjectIndex++;
        subjectName = "New Subject (" + subjectIndex + ")";
        subjectPath = folderPath + "/" + subjectName + ".asset";
      }
      
      Subject newSubject = ScriptableObject.CreateInstance<Subject>();
      AssetDatabase.CreateAsset(newSubject, subjectPath);
      AssetDatabase.SaveAssets();
      LoadSubjectObjects();
      int index = subjectObjects.FindIndex(subject => subject == newSubject);
      selectedSubjectIndex = index;
      selectedSubject = newSubject;
    }

    private void DeleteSelectedSubject()
    {
      if (selectedSubject != null)
      {
        string assetPath = AssetDatabase.GetAssetPath(selectedSubject);
        AssetDatabase.DeleteAsset(assetPath);
        subjectObjects.Remove(selectedSubject);
        selectedSubjectIndex = Mathf.Clamp(selectedSubjectIndex, 0, subjectObjects.Count - 1);
        selectedSubject = (subjectObjects.Count > 0) ? subjectObjects[selectedSubjectIndex] : null;
      }
    }

    private void SaveSelectedSubject()
    {
      if (selectedSubject != null)
      {
        EditorUtility.SetDirty(selectedSubject);
        AssetDatabase.SaveAssets();
      }
    }
}
