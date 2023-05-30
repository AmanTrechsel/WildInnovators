using UnityEngine;
using TMPro;

public class SearchScript : MonoBehaviour
{
    [SerializeField]
    private GameObject ContentHolder;
    private GameObject[] CourseSelection;
    private GameObject SearchBar;
    private int TotalCourseSelection;
    // Start is called before the first frame update
    void Start()
    {
        TotalCourseSelection = CourseSelection.transform.ChildCount;

        CourseSelection = new GameObject[TotalCourseSelection];

        for(int i = 0; i < TotalCourseSelection; i++)
        {
            CourseSelection[i] = ContentHolder.transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
