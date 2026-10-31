using System;
using VContainer;

namespace Asteroids
{
    public class GameRestarter : IDisposable
    {
        public event Action Restarted;

        private EnemyRegistry _enemiesRegistry;

        private GamePause _gamePause;
        private Player _player;
        private GameView _gameView;

        public void Dispose()
        {
            _gameView.RestartButtonClicked -= OnRestartButtonClicked;
        }

        [Inject]
        public void Construct(EnemyRegistry enemiesRegistry, GamePause gamePause)
        {
            _gamePause = gamePause;
            _enemiesRegistry = enemiesRegistry;
        }

        public void Init(Player player, GameView view)
        {
            _player = player;
            _gameView = view;

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
