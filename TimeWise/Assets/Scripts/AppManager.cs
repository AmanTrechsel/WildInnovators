using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class AppManager : MonoBehaviour
{
  // A list of all animals that can be shown in the AR
  [SerializeField]
  private List<Animal> animals;

  // Element group holding all Menu Animals
  private GameObject menuAnimalElementGroup;

  // Menu field for the animals in the menu
  [SerializeField]
  private GameObject menuAnimal;

  // Index of the currently selected animal
  private int selectedAnimal;

  // A list of all selected subject indexes
  [HideInInspector]
  public List<int> selectedSubjects;

  // Singleton
  public static AppManager Instance;

  // Period data
  private Toggle subjectFilterBiology, subjectFilterGeography, subjectFilterHistory;
  private List<Toggle> subjectFilters;
  private TextMeshProUGUI periodSliderValue;
  private Slider periodSlider;
  [HideInInspector]
  public float selectedPeriod;
  [HideInInspector]
  public Course selectedCourse;
  public GameObject arDisplayObject;
  public Subject arSubject;
  public List<int> unlockedEncyclopediaPages = new List<int>();

  // Called once at the start of the app
  private void Awake()
  {
    // Assign this instance as the singleton
    if (Instance == null) { Instance = this; }

    // Ensure this object remains active between scenes
    DontDestroyOnLoad(gameObject);

    // Setup scene based elements
    if (SceneManager.GetActiveScene().name == "Menu")
    {
      FindSceneVariables();
      UpdatePeriod();
      AddMenuAnimals();
      subjectFilters = new List<Toggle>() { subjectFilterBiology, subjectFilterGeography, subjectFilterHistory };
    }
  }

  // Basic method for loading a scene
  public void LoadScene(string sceneName)
  {
    SceneManager.LoadScene(sceneName);
  }

  // Updates every frame
  private void Update()
  {
    
  }

  // Change the currently selected animal
  public void SelectAnimal(int index)
  {
    selectedAnimal = index;
  }

  // Change the currently selected period
  public void SelectPeriod(float index)
  {
    selectedPeriod = index;
    UpdatePeriod();
  }

  // Update period data
  private void UpdatePeriod()
  {
    selectedPeriod = periodSlider.value * 1000;
    if (selectedPeriod < -5000) { selectedPeriod *= (Mathf.Floor(Mathf.Abs(periodSlider.value)/5)*10); }
    string periodString = LargeNumberToDisplayString(selectedPeriod);
    periodSliderValue.text = (selectedPeriod > 0) ? $"{periodString} na Christus" : (selectedPeriod == 0) ? $"{periodString}" : $"{LargeNumberToDisplayString(Mathf.Abs(selectedPeriod))} voor Christus";
  }

  // Update subject filter data
  public void UpdateSelectedSubjects(int subject)
  {
    bool toggleValue = subjectFilters[subject].isOn;
    if (toggleValue)
    {
      if (!selectedSubjects.Contains(subject)) { selectedSubjects.Add(subject); }
    }
    else
    {
      if (selectedSubjects.Contains(subject)) { selectedSubjects.Remove(subject); }
    }
  }

  // Change the current scene
  public void ChangeScene(string newScene)
  {
    SceneManager.LoadScene(newScene);
    if (newScene == "Menu")
    {
      FindSceneVariables();
      AddMenuAnimals();
    }
    else
    {
      ResetSceneVariables();
    }
  }

  // Add all menu animals to the element group
  private void AddMenuAnimals()
  {
    int i = 0;
    foreach (Animal animal in animals)
    {
      GameObject newMenuAnimal = Instantiate(menuAnimal, Vector3.zero, Quaternion.identity) as GameObject;
      AnimalMenuManager menuManager = newMenuAnimal.GetComponent<AnimalMenuManager>();
      menuManager.myIndex = i;
      menuManager.displayImage = animal.displayImage;
      menuManager.displayName = animal.name;
      menuManager.visiblePeriod = animal.period;
      menuManager.subject = (int)animal.subject;
      newMenuAnimal.transform.SetParent(menuAnimalElementGroup.transform, false);
      i++;
    }
  }

  // Set all period variables based on the game objects found in the scene
  private void FindSceneVariables()
  {
    menuAnimalElementGroup = GameObject.Find("AnimalContent");
    periodSliderValue = GameObject.Find("PeriodValue").GetComponent<TextMeshProUGUI>();
    periodSlider = GameObject.Find("PeriodSlider").GetComponent<Slider>();
    subjectFilterBiology = GameObject.Find("BiologyToggle").GetComponent<Toggle>();
    subjectFilterGeography = GameObject.Find("GeographyToggle").GetComponent<Toggle>();
    subjectFilterHistory = GameObject.Find("HistoryToggle").GetComponent<Toggle>();
  }

  // Reset all period variables
  private void ResetSceneVariables()
  {
    menuAnimalElementGroup = null;
    periodSliderValue = null;
    periodSlider = null;
    subjectFilterBiology = null;
    subjectFilterGeography = null;
    subjectFilterHistory = null;
  }

  // Convert a larger number into a normal format
  private string LargeNumberToDisplayString(float largeNumber)
  {
    double number = (double)largeNumber;
    string suffix = "";
    if (number >= 1e12)
    {
      number /= 1e12;
      suffix = " Biljoen";
    }
    else if (number >= 1e9)
    {
      number /= 1e9;
      suffix = " Miljard";
    }
    else if (number >= 1e6)
    {
      number /= 1e6;
      suffix = " Miljoen";
    }

    return number.ToString(number >= 10 ? "0.#" : "0.##") + suffix;
  }
}
