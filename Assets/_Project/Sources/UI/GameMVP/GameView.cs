using UnityEngine;

namespace Asteroids
{
    public class GameView : MonoBehaviour
    {
        [field: SerializeField]
        public PlayerHudView PlayerHudView { get; private set; }

        [field: SerializeField]
        public ScoreView ScoreView { get; private set; }

        [field: SerializeField]
        public GameOverView GameOverView { get; private set; }
    }
}
