using System;
using VContainer;

namespace Asteroids
{
    public class GameRestarter : IDisposable
    {
        public event Action Restarted;

        private EnemyRegistry _enemiesRegistry;

        private GameView _gameView;
        private GamePause _gamePause;
        private Player _player;

        public void Dispose()
        {
            _gameView.RestartButtonClicked -= OnRestartButtonClicked;
        }

        [Inject]
        public void Construct(
            EnemyRegistry enemiesRegistry,
            GamePause gamePause,
            Player player,
            RootUI rootUI)
        {
            _gamePause = gamePause;
            _enemiesRegistry = enemiesRegistry;
            _player = player;
            _gameView = rootUI.View;

            _gameView.RestartButtonClicked += OnRestartButtonClicked;
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
