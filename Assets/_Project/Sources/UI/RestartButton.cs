using Asteroids.GameLoop;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Asteroids.UI
{
    public class RestartButton : MonoBehaviour
    {
        private GameRestarter _gameRestarter;
        private GameObject _restartGamePanel;
        private Button _restartGameButton;

        [Inject]
        public void Construct(UIElementsHolder elementsHolder, GameRestarter gameRestarter)
        {
            _gameRestarter = gameRestarter;
            _restartGamePanel = elementsHolder.RestartGamePanel;
            _restartGameButton = elementsHolder.RestartGameButton;

            _restartGameButton.onClick.AddListener(OnRestartButtonClick);
        }

        private void OnDestroy() => _restartGameButton.onClick.RemoveAllListeners();
        private void OnRestartButtonClick()
        {
            _gameRestarter.RestartGame();
            _restartGamePanel.SetActive(false);
        }
    }
}