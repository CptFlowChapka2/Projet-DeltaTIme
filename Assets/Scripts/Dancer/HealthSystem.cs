// using System;
// using UnityEngine;
//
// public class HealthSystem : MonoBehaviour
// {
//     [SerializeField] private float invincibilityTime = 0.5f;
//     [SerializeField] private int milestoneForCrowdUpdating = 3;
//     private FeedbacksManager feedbacksManager;
//     private MeshRenderer meshRenderer;
//     public int numberOfTimeTouched = 0;
//     public int numberOfMeasuresUntouched = 0;
//     public bool isInvincible = false;
//     private int numberOfTimeTouchedLastMeasures = 0;
//     private float t;
//
//     private void Start()
//     {
//         feedbacksManager = FindFirstObjectByType<FeedbacksManager>();
//         meshRenderer = GetComponent<MeshRenderer>();
//     }
//
//     private void Update()
//     {
//         if (isInvincible)
//         {
//             meshRenderer.material.color = Color.red;
//             t += Time.deltaTime;
//             if (t >= invincibilityTime)
//             {
//                 t = 0;
//                 isInvincible = false;
//             }
//         }
//         else
//         {
//             meshRenderer.material.color = Color.blue;
//         }
//     }
//
//     public void VerifyNoDamage()
//     {
//         if (numberOfTimeTouched == numberOfTimeTouchedLastMeasures)
//         {
//             numberOfMeasuresUntouched++;
//         }
//         else
//         {
//             numberOfMeasuresUntouched = 0;
//         }
//         
//         numberOfTimeTouchedLastMeasures = numberOfTimeTouched;
//
//         if (numberOfMeasuresUntouched == milestoneForCrowdUpdating)
//         {
//             numberOfMeasuresUntouched = 0;
//             feedbacksManager.CrowdUpdatingFeedback(this.gameObject, false);
//         }
//     }
// }
