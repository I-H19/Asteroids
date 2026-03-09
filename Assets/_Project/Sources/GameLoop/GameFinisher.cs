using Asteroids.Gameplay;
using Asteroids.UI;
using UnityEngine;
using VContainer;

namespace Asteroids.GameLoop
{
    public class GameFinisher : MonoBehaviour
    {
        private UIElementsHolder _uiElementsHolder;
        private GamePause _gamePause;
        private Player _player;

        [Inject]
        public void Construct(UIElementsHolder uIElementsHolder, GamePause gamePause, Player player)
        {
            _uiElementsHolder = uIElementsHolder;
            _gamePause = gamePause;

            player.Death += Finish;

            _player = player;
        }

        public void Finish()
        {
            _uiElementsHolder.RestartGamePanel.SetActive(true);
            _gamePause.SetPause(true);
        }

        private void OnDestroy() => _player.Death -= Finish;
    }
}

