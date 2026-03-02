using UnityEngine;

public class FeedbacksManager : MonoBehaviour
{
    [SerializeField] private InputTrackerAndPatternSpawner player1;
    [SerializeField] private InputTrackerAndPatternSpawner player2;
    private float actualP1Level = 0f;
    private float actualP2Level = 0f;
    private float numberOfMeasuresP1Untouched = 0f;
    private float numberOfMeasuresP2Untouched = 0f;
}
