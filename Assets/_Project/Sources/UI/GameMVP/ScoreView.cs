using TMPro;
using UnityEngine;

namespace Asteroids
{
    public class ScoreView : MonoBehaviour
    {
        [field: SerializeField]
        public TextMeshProUGUI RuntimeScore { get; private set; }

        [field: SerializeField]
        public TextMeshProUGUI ResultScore { get; private set; }

        public void ChangeScore(int score)
        {
            RuntimeScore.text = $"GameScore: {score}";
            ResultScore.text = $"GameScore: {score}";
        }
    }
}