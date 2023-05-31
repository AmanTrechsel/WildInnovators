using UnityEngine;
using TMPro;

public class SearchScript : MonoBehaviour
{
    [SerializeField]
    private GameObject ContentHolder, SearchBar;
    [SerializeField]
    private GameObject[] CourseSelection;
    [SerializeField]
    private int TotalCourseSelection;

    void Start()
    {
        TotalCourseSelection = CourseSelection[0].transform.childCount;

        CourseSelection = new GameObject[TotalCourseSelection];

        for(int i = 0; i < TotalCourseSelection; i++)
        {
            CourseSelection[i] = ContentHolder.transform.GetChild(i).gameObject;
        }
    }
}
