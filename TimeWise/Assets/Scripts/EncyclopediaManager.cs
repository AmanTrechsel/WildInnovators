using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EncyclopediaManager : MonoBehaviour
{
  // Singleton
  public static EncyclopediaManager Instance;

  // The unlock count text
  [SerializeField]
  private TextMeshProUGUI unlockCount;
  // The subject name text
  [SerializeField]
  private TextMeshProUGUI subjectName;
  // The encyclopedia page content transform
  [SerializeField]
  private Transform encyclopediaPageContent;
  // The encyclopedia page button prefab
  [SerializeField]
  private GameObject encyclopediaPageButtonPrefab;

  [Header("Page Extra Info")]
  // The page panel
  [SerializeField]
  private GameObject pagePanel;
  // The page image
  [SerializeField]
  private Image pageImage;
  // The page title
  [SerializeField]
  private TextMeshProUGUI pageTitle;
  // The page description
  [SerializeField]
  private TextMeshProUGUI pageDescription;
  
  // Called once at the start of the app
  private void Awake()
  {
    // Assign this instance as the singleton
    if (Instance == null) { Instance = this; }
    else if (Instance != this) { Destroy(gameObject); }

    // Set the unlock count and subject name
    unlockCount.text = $"{AppManager.Instance.unlockedEncyclopediaPages.Count}/{ResourceManager.Instance.encyclopediaPages.Count}";
    subjectName.text = $"{AppManager.Instance.arSubject.name}";

    // Iterate through all encyclopedia pages found in ResourceManager of the current subject
    foreach (EncyclopediaPage encyclopediaPage in ResourceManager.Instance.GetEncyclopediaPagesBySubject(AppManager.Instance.arSubject))
    {
      // Create a new encyclopedia page button based on the given prefab
      GameObject pageToAdd = Instantiate(encyclopediaPageButtonPrefab) as GameObject;
      pageToAdd.GetComponent<EncyclopediaPageButton>().SetPage(encyclopediaPage);
      pageToAdd.transform.SetParent(encyclopediaPageContent);
    }
  }

  // Show the encyclopedia page
  public void ShowPage(EncyclopediaPage pageToShow)
  {
    // Set the page panel active
    pagePanel.SetActive(true);

    // Check if the page is unlocked
    if (AppManager.Instance.unlockedEncyclopediaPages.Contains((int)pageToShow.id))
    {
      // Set the page image, title and description to the page to show
      pageImage.sprite = pageToShow.displayImage;
      pageImage.color = Color.white;
      pageTitle.text = pageToShow.displayName;
      pageDescription.text = pageToShow.description;
    }
    else
    {
      // Set the page image, title and description to unknown
      pageImage.sprite = null;
      pageImage.color = Color.black;
      pageTitle.text = "???";
      pageDescription.text = "Onbekend, leer meer over dit dier om meer informatie te verkrijgen.";
    }
  }

  // Hide the encyclopedia page
  public void HidePage()
  {
    // Set the page panel inactive
    pagePanel.SetActive(false);
  }
}
