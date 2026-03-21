using System;
using _Project.Sources.Config;
using _Project.Sources.GameLoop;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Project.Sources.UI.GUI
{
    [RequireComponent(typeof(Button))]
    public class RestartButton : MonoBehaviour
    {
        public Action Clicked;
        
        private GameRestarter _gameRestarter;
        private Button _restartGameButton;

        [Inject]
        public void Construct(GameRestarter gameRestarter) => _gameRestarter = gameRestarter;
        
        public void Init()
        {
            _restartGameButton = gameObject.GetComponent<Button>();
            _restartGameButton.onClick.AddListener(OnRestartButtonClick);
        }

        private void OnDestroy() => _restartGameButton.onClick.RemoveAllListeners();
        private void OnRestartButtonClick()
        {
            Clicked?.Invoke();
            _gameRestarter.RestartGame();
        }
    }
}