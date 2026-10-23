using System;
using UnityEngine.UI;
using VContainer;

namespace Asteroids
{
    public class GameRestarter : IDisposable
    {
        private EnemyRegistry _enemiesRegistry;

        private GamePause _gamePause;
        private Button _gameRestartButton;
        private Player _player;

        public void Dispose()
        {
            _gameRestartButton.onClick.RemoveAllListeners();
        }

        public event Action Restarted;


        [Inject]
        public void Construct(EnemyRegistry enemiesRegistry, GamePause gamePause)
        {
            _gamePause = gamePause;
            _enemiesRegistry = enemiesRegistry;
        }

        public void Init(Player player, Button restartButton)
        {
            _player = player;
            _gameRestartButton = restartButton;

            _gameRestartButton.onClick.AddListener(OnRestartButtonClicked);
        }

        private void OnRestartButtonClicked()
        {
            _enemiesRegistry.KillAll();
            _player.ResetPlayer();

            Restarted?.Invoke();

            _gamePause.SetPause(false);
        }
    }
}