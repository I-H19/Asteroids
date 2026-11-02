using System;
using VContainer;

namespace Asteroids
{
    public class GameFinisher : IDisposable
    {
        private Player _player;
        private GamePause _gamePause;

        public event Action Finished;

        [Inject]
        public void Construct(Player player, GamePause gamePause)
        {
            _player = player;
            _gamePause = gamePause;
            player.Died += OnPlayerDied;
        }

        public void Dispose()
        {
            _player.Died -= OnPlayerDied;
        }

        private void OnPlayerDied()
        {
            Finished?.Invoke();
            _gamePause.SetPause(true);
        }
    }
}