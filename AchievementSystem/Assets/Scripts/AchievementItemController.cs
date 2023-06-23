using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementItemController : MonoBehaviour
{
  [SerializeField]
  // Reference to the unlocked icon image
  Image unlockedIcon; 

  [SerializeField]
  // Reference to the locked icon image
  Image lockedIcon; 

  [SerializeField]
  // Reference to the title label text component
  Text titleLabel; 

  [SerializeField]
  // Reference to the description label text component
  Text descriptionLabel; 

  // Flag indicating whether the achievement is unlocked
  public bool unlocked; 

  // Reference to the Achievement data for this item
  public Achievement achievement; 

  public void RefreshView()
  {
    // Set the title label text to the achievement's title
    titleLabel.text = achievement.title; 
    // Set the description label text to the achievement's description
    descriptionLabel.text = achievement.description; 

    // Enable the unlocked icon if the achievement is unlocked
    unlockedIcon.enabled = unlocked; 
    // Enable the locked icon if the achievement is locked
    lockedIcon.enabled = !unlocked; 
  }

  private void OnValidate()
  {
    // Automatically refresh the view when the script is validated in the Unity editor
    RefreshView(); 
  }
}