using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EncyclopediaManager : MonoBehaviour
{
  public static EncyclopediaManager Instance;

  [SerializeField]
  private TextMeshProUGUI unlockCount;
  [SerializeField]
  private TextMeshProUGUI subjectName;
  [SerializeField]
  private Transform encyclopediaPageContent;
  [SerializeField]
  private GameObject encyclopediaPageButtonPrefab;
  [Header("Page Extra Info")]
  [SerializeField]
  private GameObject pagePanel;
  [SerializeField]
  private Image pageImage;
  [SerializeField]
  private TextMeshProUGUI pageTitle;
  [SerializeField]
  private TextMeshProUGUI pageDescription;
  
  private void Awake()
  {
    if (Instance == null) { Instance = this; }
    unlockCount.text = $"{AppManager.Instance.unlockedEncyclopediaPages.Count}/{ResourceManager.Instance.encyclopediaPages.Count}";
    subjectName.text = $"{AppManager.Instance.arSubject.name}";
    foreach (EncyclopediaPage encyclopediaPage in ResourceManager.Instance.GetEncyclopediaPagesBySubject(AppManager.Instance.arSubject))
    {
      GameObject pageToAdd = Instantiate(encyclopediaPageButtonPrefab) as GameObject;
      pageToAdd.GetComponent<EncyclopediaPageButton>().SetPage(encyclopediaPage);
      pageToAdd.transform.SetParent(encyclopediaPageContent);
    }
  }

  public void ShowPage(EncyclopediaPage pageToShow)
  {
    pagePanel.SetActive(true);
    pageImage.sprite = pageToShow.displayImage;
    pageTitle.text = pageToShow.displayName;
    pageDescription.text = pageToShow.description;
  }

  public void HidePage()
  {
    pagePanel.SetActive(false);
  }
}
