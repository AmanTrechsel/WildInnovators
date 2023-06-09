using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SearchScript : MonoBehaviour
{
  // The input field for the search bar
  [SerializeField]
  private TMP_InputField searchBar;
  // The content list for the search items
  [SerializeField]
  private Transform content;
  // Rect transform for the content
  [SerializeField]
  private RectTransform contentRect;
  // Vertical layout group for the content
  [SerializeField]
  private VerticalLayoutGroup contentLayoutGroup;
  // The prefab for the search items
  [SerializeField]
  private GameObject searchItemPrefab;
  // The titles for the different search types
  [SerializeField]
  private GameObject courseTitle, subjectTitle, settingTitle, encyclopediaTitle;

  // Unique search items for each searchable element
  private Dictionary<string, GameObject> searchCourseItems = new Dictionary<string, GameObject>();
  private Dictionary<string, GameObject> searchSubjectItems = new Dictionary<string, GameObject>();
  private Dictionary<string, GameObject> searchSettingItems = new Dictionary<string, GameObject>();
  private Dictionary<string, GameObject> searchEncyclopediaItems = new Dictionary<string, GameObject>();

  // The number of items found for each searchable element
  private int coursesFound, subjectsFound, settingsFound, encyclopediaFound;

  // Called once at the start of the app
  void Awake()
  {
    // Add title to the bottom of content
    courseTitle.transform.SetSiblingIndex(content.childCount - 1);

    // Iterate through all courses found in ResourceManager
    foreach (Course course in ResourceManager.Instance.courses)
    {
      // Create a new course button based on the given prefab
      GameObject createdCourseButton = Instantiate(searchItemPrefab) as GameObject;
      // Set the current course to the button
      createdCourseButton.GetComponent<SearchButton>().SetCourse(course);
      // Add the created course button to the content list
      createdCourseButton.transform.SetParent(content);
      // Store the created GameObject to search items
      searchCourseItems.Add(course.name, createdCourseButton);
    }

    // Add title to the bottom of content
    subjectTitle.transform.SetSiblingIndex(content.childCount - 1);

    // Iterate through all subjects found in ResourceManager
    foreach (Subject subject in ResourceManager.Instance.subjects)
    {
      // Create a new subject button based on the given prefab
      GameObject subjectButton = Instantiate(searchItemPrefab) as GameObject;
      // Set the current subject to the button
      subjectButton.GetComponent<SearchButton>().SetSubject(subject);
      // Add the created subject button to the content list
      subjectButton.transform.SetParent(content);
      // Store the created GameObject to search items
      searchSubjectItems.Add(subject.name, subjectButton);
    }

    // Add title to the bottom of content
    settingTitle.transform.SetSiblingIndex(content.childCount - 1);

    // Iterate through all settings found in ResourceManager
    foreach (Setting setting in ResourceManager.Instance.settings)
    {
      // Create a new setting button based on the given prefab
      GameObject createdSettingButton = Instantiate(searchItemPrefab) as GameObject;
      // Set the current setting to the button
      createdSettingButton.GetComponent<SearchButton>().SetSetting(setting);
      // Add the created setting button to the content list
      createdSettingButton.transform.SetParent(content);
      // Store the created GameObject to search items
      searchSettingItems.Add(setting.name, createdSettingButton);
    }

    // Add title to the bottom of content
    encyclopediaTitle.transform.SetSiblingIndex(content.childCount - 1);

    // Iterate through all encyclopedia pages found in ResourceManager
    foreach (EncyclopediaPage encyclopediaPage in ResourceManager.Instance.encyclopediaPages)
    {
      // Check if the current encyclopedia page has been unlocked
      if (AppManager.Instance.unlockedEncyclopediaPages.Contains((int)encyclopediaPage.id))
      {
        // Create a new encyclopedia page button based on the given prefab
        GameObject pageToAdd = Instantiate(searchItemPrefab) as GameObject;
        // Set the current encyclopedia page to the button
        pageToAdd.GetComponent<SearchButton>().SetPage(encyclopediaPage);
        // Add the created encyclopedia button to the content list
        pageToAdd.transform.SetParent(content);
        // Store the created GameObject to search items
        searchEncyclopediaItems.Add(encyclopediaPage.displayName, pageToAdd);
      }
    }

    // Reset the search term at start
    ChangeSearch();
  }

  // This is the function that is called when the search button is pressed
  public void ChangeSearch()
  {
    // Get the search entry value from the search bar
    string searchEntry = searchBar.text;

    // Reset search results
    coursesFound = 0;
    subjectsFound = 0;
    settingsFound = 0;
    encyclopediaFound = 0;

    // Go through each course
    foreach (string _name in searchCourseItems.Keys)
    {
      // Check if the name fits within the search entry
      if (CheckSearch(_name, searchEntry))
      {
        // Show the entry item
        searchCourseItems[_name].SetActive(true);
        // Add to the total courses found
        coursesFound++;
      }
      else
      {
        // Hide the entry item
        searchCourseItems[_name].SetActive(false);
      }
    }

    // Go through each subject
    foreach (string _name in searchSubjectItems.Keys)
    {
      // Check if the name fits within the search entry
      if (CheckSearch(_name, searchEntry))
      {
        // Show the entry item
        searchSubjectItems[_name].SetActive(true);
        // Add to the total courses found
        subjectsFound++;
      }
      else
      {
        // Hide the entry item
        searchSubjectItems[_name].SetActive(false);
      }
    }

    // Go through each setting
    foreach (string _name in searchSettingItems.Keys)
    {
      // Check if the name fits within the search entry
      if (CheckSearch(_name, searchEntry))
      {
        // Show the entry item
        searchSettingItems[_name].SetActive(true);
        // Add to the total courses found
        settingsFound++;
      }
      else
      {
        // Hide the entry item
        searchSettingItems[_name].SetActive(false);
      }
    }

    // Go through each encyclopedia page
    foreach (string _name in searchEncyclopediaItems.Keys)
    {
      // Check if the name fits within the search entry
      if (CheckSearch(_name, searchEntry))
      {
        // Show the entry item
        searchEncyclopediaItems[_name].SetActive(true);
        // Add to the total courses found
        encyclopediaFound++;
      }
      else
      {
        // Hide the entry item
        searchEncyclopediaItems[_name].SetActive(false);
      }
    }

    // Show titles if any were found
    courseTitle.SetActive(coursesFound > 0);
    subjectTitle.SetActive(subjectsFound > 0);
    settingTitle.SetActive(settingsFound > 0);
    encyclopediaTitle.SetActive(encyclopediaFound > 0);

    // Setup the content's height with the padding
    float contentHeight = contentLayoutGroup.padding.top + contentLayoutGroup.padding.bottom;

    // Update the content's height to be the sum of all the items' heights
    foreach (RectTransform child in content)
    {
      // Check if the child is active
      if (!child.gameObject.activeSelf) { continue; }

      // Add the height of the child along with the padding multiplied by its scale to the content's height
      contentHeight += (child.sizeDelta.y + contentLayoutGroup.spacing);
    }

    // 60% multiplier to the content's height
    contentHeight *= 0.6f;

    // Set the content's height to contentHeight
    contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, contentHeight);
  }

  // This function checks if the search term is within the term
  private bool CheckSearch(string term, string searchTerm)
  {
    // Check if the search term is within the term
    return searchTerm.ToLower() == term.Substring(0,searchTerm.Length).ToLower();
  }
}
