using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids
{
    public class RootUI : MonoBehaviour
    {
        [field: SerializeField]
        public GameView View { get; private set; }

        [field: SerializeField]
        public RestartPanel GameRestartPanel { get; private set; }

        [field: SerializeField]
        public TextMeshProUGUI RuntimeScore { get; private set; }

        [field: SerializeField]
        public TextMeshProUGUI ResultScore { get; private set; }

        [field: SerializeField]
        public TextMeshProUGUI PlayerStats { get; private set; }

        [field: SerializeField]
        public Button RestartButton { get; private set; }
    }
}