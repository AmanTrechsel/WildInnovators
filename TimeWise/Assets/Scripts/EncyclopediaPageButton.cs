using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EncyclopediaPageButton : MonoBehaviour
{
  [SerializeField]
  private Image pageDisplayImage;
  [SerializeField]
  private TextMeshProUGUI pageName;

  private EncyclopediaPage _page;

  public void SetPage(EncyclopediaPage setPage)
  {
    _page = setPage;
    if (AppManager.Instance.unlockedEncyclopediaPages.Contains((int)_page.id))
    {
      pageName.text = _page.displayName;
      pageDisplayImage.sprite = _page.displayImage;
    }
    else
    {
      pageName.text = "???";
      pageDisplayImage.enabled = false;
    }
  }

  public void ShowPageInfo()
  {

  }
}
