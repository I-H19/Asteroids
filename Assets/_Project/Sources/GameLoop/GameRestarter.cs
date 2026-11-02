using System;
using VContainer;

namespace Asteroids
{
    public class GameRestarter : IDisposable
    {
        private EnemyRegistry _enemiesRegistry;
        private BulletRegistry _bulletRegistry;

        private GameOverView _gameOverView;
        private GamePause _gamePause;
        private Player _player;
        public event Action Restarted;

        [Inject]
        public void Construct(
            EnemyRegistry enemiesRegistry,
            BulletRegistry bulletRegistry,
            GamePause gamePause,
            Player player,
            GameView gameView)
        {
            _gamePause = gamePause;
            _enemiesRegistry = enemiesRegistry;
            _bulletRegistry = bulletRegistry;
            _player = player;

            _gameOverView = gameView.GameOverView;
            _gameOverView.RestartButtonClicked += OnRestartButtonClicked;
        }

        public void Dispose()
        {
            _gameOverView.RestartButtonClicked -= OnRestartButtonClicked;
        }

        private void OnRestartButtonClicked()
        {
            _bulletRegistry.ClearAll();
            _enemiesRegistry.KillAll();
            _player.ResetPlayer();

            Restarted?.Invoke();

            _gamePause.SetPause(false);
        }
    }
}
