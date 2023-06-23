using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
  // Reference to the AchievementDatabase
  public AchievementDatabase database; 

  // Reference to the AchievementNotificationController
  public AchievementNotificationController achievementNotificationController; 

  // Reference to the AchievementDropdownController
  public AchievementDropdownController achievementDropdownController; 

  // Prefab for the AchievementItemController
  public GameObject achievementItemPrefab; 
  // Parent transform for the instantiated achievement item objects
  public Transform scrollViewContent; 

  // ID of the achievement to show in the notification
  public AchievementID achievementToShow; 

  [SerializeField]
  [HideInInspector]
  // List of instantiated AchievementItemControllers
  private List<AchievementItemController> achievementItems; 

  private void Start()
  {
    // Subscribe to the dropdown value changed event
    achievementDropdownController.onValueChanged += HandleAchievementDropdownValueChanged; 
    // Load and display the achievements table
    LoadAchievementsTable(); 
  }

  public void ShowNotification()
  {
    // Get the Achievement object based on the achievementToShow ID
    Achievement achievement = database.achievements[(int)achievementToShow]; 
    // Show the notification for the given achievement
    achievementNotificationController.ShowNotification(achievement); 
  }

  private void HandleAchievementDropdownValueChanged(AchievementID achievement)
  {
    // Update the achievementToShow with the selected achievement from the dropdown
    achievementToShow = achievement; 
  }

  [ContextMenu("LoadAchievementsTable()")]
  private void LoadAchievementsTable()
  {
    foreach (AchievementItemController item in achievementItems)
    {
      // Destroy existing achievement item objects
      DestroyImmediate(item.gameObject); 
    }

    // Clear the list of achievement items
    achievementItems.Clear(); 

    // Iterate through each achievement in the database
    foreach (Achievement achievement in database.achievements) 
    {
      // Instantiate the achievement item object
      GameObject obj = Instantiate(achievementItemPrefab, scrollViewContent); 
      // Get the AchievementItemController component
      AchievementItemController item = obj.GetComponent<AchievementItemController>(); 
      // Check if the achievement is unlocked based on its ID in the PlayerPrefs
      bool unlocked = PlayerPrefs.GetInt(achievement.id, 0) == 1; 
      // Set the unlocked status of the achievement item
      item.unlocked = unlocked; 
      // Set the achievement data for the achievement item
      item.achievement = achievement; 
      // Refresh the view of the achievement item
      item.RefreshView(); 
      // Add the achievement item to the list
      achievementItems.Add(item); 
    }
  }

  public void UnlockAchievement()
  {
    // Unlock the currently selected achievement from the dropdown
    UnlockAchievement(achievementToShow); 
  }

  public void UnlockAchievement(AchievementID achievement)
  {
    // Get the AchievementItemController for the specified achievement
    AchievementItemController item = achievementItems[(int)achievement]; 
    // If the achievement is already unlocked, return
    if (item.unlocked) { return; } 

    // Show the notification for the unlocked achievement
    ShowNotification(); 
    // Set the achievement ID in PlayerPrefs to indicate it's unlocked
    PlayerPrefs.SetInt(item.achievement.id, 1); 
    // Set the unlocked status of the achievement item
    item.unlocked = true; 
    // Refresh the view of the achievement item
    item.RefreshView(); 
  }

  public void LockAllAchievements()
  {
    // Iterate through each achievement in the database
    foreach (Achievement achievement in database.achievements) 
    {
      // Remove the achievement ID from PlayerPrefs to indicate it's locked
      PlayerPrefs.DeleteKey(achievement.id); 
    }

    // Iterate through each achievement item
    foreach (AchievementItemController item in achievementItems) 
    {
      // Set the unlocked status of the achievement item to false
      item.unlocked = false; 
      // Refresh the view of the achievement item
      item.RefreshView(); 
    }
  }
}
