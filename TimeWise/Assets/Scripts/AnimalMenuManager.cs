using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnimalMenuManager : MonoBehaviour
{
  // Assigned by the app manager
  [HideInInspector]
  public int myIndex;
  [HideInInspector]
  public Sprite displayImage;
  [HideInInspector]
  public string displayName;
  [HideInInspector]
  public Vector2 visiblePeriod;
  [HideInInspector]
  public int subject;

  // References to own components
  [SerializeField]
  private Image spriteField;
  [SerializeField]
  private TextMeshProUGUI nameField;
  [SerializeField]
  private LayoutElement layoutElement;

  // Called once at the start of the object
  private void Start()
  {
    UpdateFields();
  }

  // Updates every frame
  private void Update()
  {
    bool isVisible = WithinVisiblePeriod() && SubjectFiltered();
    transform.GetChild(0).gameObject.SetActive(isVisible);
    layoutElement.ignoreLayout = !isVisible;
  }

  // Check if the selected period is within this animal's filter period
  private bool WithinVisiblePeriod()
  {
    float period = AppManager.appManager.selectedPeriod;
    return period >= visiblePeriod.x && period <= visiblePeriod.y;
  }

  // Check if this animal's filter subject is selected
  private bool SubjectFiltered()
  {
    return AppManager.appManager.selectedSubjects.Contains(subject);
  }

  // Updates all shown fields
  private void UpdateFields()
  {
    spriteField.sprite = displayImage;
    nameField.text = displayName;
  }

  // Called when this animal is selected
  public void AnimalClicked()
  {
    AppManager.appManager.SelectAnimal(myIndex);
  }
}
