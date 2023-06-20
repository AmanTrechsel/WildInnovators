using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class CourseEditorWindow : EditorWindow
{
  private List<Course> courseObjects = new List<Course>();
  private Course selectedCourse;
  private int selectedCourseIndex;
  private Vector2 scrollPos;

  [MenuItem("Custom/Course Editor")]
  public static void ShowWindow()
  {
    EditorWindow.GetWindow<CourseEditorWindow>("Course Editor");
  }

  private void OnEnable()
  {
    LoadCourseObjects();
  }

  private void OnGUI()
  {
    GUILayout.BeginHorizontal();
    GUILayout.BeginVertical(GUILayout.Width(200));
    GUILayout.Label("Course List", EditorStyles.boldLabel);
    scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.ExpandWidth(true));
    for (int i = 0; i < courseObjects.Count; i++)
    {
      if (GUILayout.Toggle(i == selectedCourseIndex, courseObjects[i].name, "Button"))
      {
        if (selectedCourseIndex != i)
        {
          selectedCourseIndex = i;
          selectedCourse = courseObjects[i];
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
      CreateNewCourse();
      GUI.FocusControl(null);
    }

    if (selectedCourse != null && GUILayout.Button("-"))
    {
      DeleteSelectedCourse();
      GUI.FocusControl(null);
    }

    GUILayout.EndHorizontal();
    GUILayout.EndVertical();
    GUILayout.Box("", GUILayout.ExpandHeight(true), GUILayout.Width(5));
    if (selectedCourse != null)
    {
      GUILayout.BeginVertical();
      GUILayout.Label("Selected Course", EditorStyles.boldLabel);
      string newFilename = EditorGUILayout.TextField("Filename", selectedCourse.name);
      if (newFilename != selectedCourse.name)
      {
        string newPath = AssetDatabase.GetAssetPath(selectedCourse);
        string newFullPath = newPath.Replace(selectedCourse.name + ".asset", newFilename + ".asset");
        AssetDatabase.RenameAsset(newPath, newFilename);
        AssetDatabase.SaveAssets();
        selectedCourse.name = newFilename;
      }

      Editor editor = Editor.CreateEditor(selectedCourse);
      editor.DrawDefaultInspector();
      GUILayout.Space(20);
      if (selectedCourse != null && GUILayout.Button("Highlight Selected Course"))
      {
        EditorGUIUtility.PingObject(selectedCourse);
      }

      GUILayout.EndVertical();
    }
    GUILayout.EndHorizontal();
  }

    private void LoadCourseObjects()
    {
      courseObjects.Clear();
      string[] assetGUIDs = AssetDatabase.FindAssets("t:Course", new[] { "Assets/Resources/Courses" });
      foreach (string assetGUID in assetGUIDs)
      {
        string assetPath = AssetDatabase.GUIDToAssetPath(assetGUID);
        Course course = AssetDatabase.LoadAssetAtPath<Course>(assetPath);
        if (course != null)
        {
          courseObjects.Add(course);
        }
      }

      if (courseObjects.Count > 0)
      {
        selectedCourseIndex = 0;
        selectedCourse = courseObjects[0];
      }
    }

    private void CreateNewCourse()
    {
      string folderPath = "Assets/Resources/Courses";
      string courseName = "New Course";
      string coursePath = folderPath + "/" + courseName + ".asset";
      int courseIndex = 1;
      while (AssetDatabase.LoadAssetAtPath<Course>(coursePath) != null)
      {
        courseIndex++;
        courseName = "New Course (" + courseIndex + ")";
        coursePath = folderPath + "/" + courseName + ".asset";
      }
      
      Course newCourse = ScriptableObject.CreateInstance<Course>();
      AssetDatabase.CreateAsset(newCourse, coursePath);
      AssetDatabase.SaveAssets();
      LoadCourseObjects();
      int index = courseObjects.FindIndex(course => course == newCourse);
      selectedCourseIndex = index;
      selectedCourse = newCourse;
    }

    private void DeleteSelectedCourse()
    {
      if (selectedCourse != null)
      {
        string assetPath = AssetDatabase.GetAssetPath(selectedCourse);
        AssetDatabase.DeleteAsset(assetPath);
        courseObjects.Remove(selectedCourse);
        selectedCourseIndex = Mathf.Clamp(selectedCourseIndex, 0, courseObjects.Count - 1);
        selectedCourse = (courseObjects.Count > 0) ? courseObjects[selectedCourseIndex] : null;
      }
    }

    private void SaveSelectedCourse()
    {
      if (selectedCourse != null)
      {
        EditorUtility.SetDirty(selectedCourse);
        AssetDatabase.SaveAssets();
      }
    }
}
