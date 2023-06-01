using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EncyclopediaPageButton : MonoBehaviour
{
  // The page display image for this button
  [SerializeField]
  private Image pageDisplayImage;
  // The text field for the page name
  [SerializeField]
  private TextMeshProUGUI pageName;
  
  // The page this button represents
  private EncyclopediaPage _page;

  // Set the page for this button
  public void SetPage(EncyclopediaPage setPage)
  {
    // Set the page and update the button
    _page = setPage;

    // Check if the page is unlocked
    if (AppManager.Instance.unlockedEncyclopediaPages.Contains((int)_page.id))
    {
      // Set the page name and display image
      pageName.text = _page.displayName;
      pageDisplayImage.sprite = _page.displayImage;
    }
    else
    {
      // Set the page name to ??? and disable the display image
      pageName.text = "???";
      pageDisplayImage.enabled = false;
    }
  }

  // Show the page info
  public void ShowPageInfo()
  {
    EncyclopediaManager.Instance.ShowPage(_page);
  }
}
