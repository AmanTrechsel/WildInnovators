using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class ResourceManager : MonoBehaviour
{
  // Singleton
  public static ResourceManager Instance;

  // Lists containing all resources found in the Assets/Resources folder
  public List<ARItem> arItems = new List<ARItem>();
  public List<Subject> subjects = new List<Subject>();
  public List<Course> courses = new List<Course>();
  public List<EncyclopediaPage> encyclopediaPages = new List<EncyclopediaPage>();
  public List<Setting> settings = new List<Setting>();

  // Called once at the start of the app
  private void Awake()
  {
    // Assign this instance as the singleton
    if (Instance == null) { Instance = this; }
    else if (Instance != this) { Destroy(gameObject); }

    // Ensure this object remains active between scenes
    DontDestroyOnLoad(gameObject);

    // Calls the method that adds a SphereCollider to every ARItem
    AddCollider();

    // Load and store found resources
    arItems.AddRange(Resources.LoadAll<ARItem>("ARItems"));
    subjects.AddRange(Resources.LoadAll<Subject>("Subjects"));
    courses.AddRange(Resources.LoadAll<Course>("Courses"));
    encyclopediaPages.AddRange(Resources.LoadAll<EncyclopediaPage>("EncyclopediaPages"));
    settings.AddRange(Resources.LoadAll<Setting>("Settings"));
  }

  // Returns a subject from its id
  public Subject GetSubjectByID(int id)
  {
    // Iterate through all subjects
    foreach (Subject subject in subjects)
    {
      // Check if this subject's id is the same as the given id
      if (subject.id == (uint)id)
      {
        // Return this subject if it has the given id
        return subject;
      }
    }
    // None with this id were found
    return null;
  }

  // Returns a course from its id
  public Course GetCourseByID(int id)
  {
    // Iterate through all courses
    foreach (Course course in courses)
    {
      // Check if this course's id is the same as the given id
      if (course.id == (uint)id)
      {
        // Return this course if it has the given id
        return course;
      }
    }
    // None with this id were found
    return null;
  }

  // Returns a course from its subject
  public Course FindCourseContainingSubject(Subject subject)
  {
    // Iterate through all courses
    foreach (Course course in courses)
    {
      // Check if this course contains the given subject
      if (course.subjects.Contains(subject))
      {
        // Return this course if it contains the given subject
        return course;
      }
    }
    // None with this subject were found
    return null;
  }

  // Returns an encyclopedia page from its id
  public EncyclopediaPage GetEncyclopediaPageByID(int id)
  {
    // Iterate through all encyclopedia pages
    foreach (EncyclopediaPage encyclopediaPage in encyclopediaPages)
    {
      // Check if this encyclopedia page's id is the same as the given id
      if (encyclopediaPage.id == (uint)id)
      {
        // Return this encyclopedia page if it has the given id
        return encyclopediaPage;
      }
    }
    // None with this id were found
    return null;
  }

  // Returns a list of all Encyclopedia Pages that the subject's topic covers
  public List<EncyclopediaPage> GetEncyclopediaPagesBySubject(Subject subject)
  {
    // Initiate a list that will be returned at the end
    List<EncyclopediaPage> subjectPages = new List<EncyclopediaPage>();
    // Iterate through all encyclopedia pages
    foreach (EncyclopediaPage page in encyclopediaPages)
    {
      // Check if this pages's subject is the same as the given subject
      if (page.subject == subject)
      {
        // Add this page to the list if it is part of this subject
        subjectPages.Add(page);
      }
    }
    // Return the list of encyclopedia pages with this topic
    return subjectPages;
  }

  // Adds a sphere collider on every AR-Item
  public void AddCollider()
  {
    List<GameObject> arObjects = new List<GameObject>();

    foreach(ARItem arItem in arItems)
    {
      GameObject arObject = Instantiate(arItem.prefab) as GameObject;
      arObjects.Add(arObject);
    } 

    foreach(GameObject arGameObject in arObjects)
    {
      arGameObject.AddComponent<SphereCollider>();
    }
  }
}
