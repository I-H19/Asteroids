using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Sources.UI
{
    public class UIElementsHolder : MonoBehaviour
    {
        [field: SerializeField] public TextMeshProUGUI PlayerScoreGameText { get; private set; }
        [field: SerializeField] public TextMeshProUGUI PlayerScoreResultText { get; private set; }
        [field: SerializeField] public TextMeshProUGUI PlayerMovementDataText { get; private set; }
        [field: SerializeField] public GameObject RestartGamePanel { get; private set; }
        [field: SerializeField] public Button RestartGameButton { get; private set; }
    }
}
