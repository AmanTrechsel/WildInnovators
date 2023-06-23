using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class AchievementNotificationController : MonoBehaviour
{
  [SerializeField]
  // Reference to the title label text component
  Text achievementTitleLabel; 

  // Reference to the Animator component
  private Animator m_animator; 

  private void Awake()
  {
    // Get the Animator component attached to the same GameObject
    m_animator = GetComponent<Animator>(); 
  }

  public void ShowNotification(Achievement achievement)
  {
    // Set the text of the achievement title label to the achievement's title
    achievementTitleLabel.text = achievement.title; 
    // Trigger the "Appear" animation in the Animator
    m_animator.SetTrigger("Appear"); 
  }
}
