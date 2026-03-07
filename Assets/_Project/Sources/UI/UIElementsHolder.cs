using TMPro;
using UnityEngine;

public class UIElementsHolder : MonoBehaviour
{
    [field: SerializeField] public TextMeshProUGUI PlayerScoreGameText { get; private set; }
    [field: SerializeField] public TextMeshProUGUI PlayerScoreResultText { get; private set; }
    [field: SerializeField] public TextMeshProUGUI PlayerMovementDataText { get; private set; }
    [field: SerializeField] public GameObject RestartGamePanel { get; private set; }
}
