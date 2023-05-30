using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ClickingOnSearch : MonoBehaviour
{
    [SerializeField]
    private GameObject searchbar, searchbarLogo;

    public void ShowSearchBar()
    {
        searchbar.SetActive(true);
        searchbarLogo.SetActive(false);
    }

}
