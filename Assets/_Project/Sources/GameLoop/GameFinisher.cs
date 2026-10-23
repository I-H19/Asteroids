using System;
using VContainer;

namespace Asteroids
{
    public class GameFinisher : IDisposable
    {
        private GamePause _gamePause;
        private Player _player;

        public void Dispose()
        {
            _player.Died -= OnPlayerDied;
        }

        public event Action Finished;

        [Inject]
        public void Construct(GamePause gamePause)
        {
            _gamePause = gamePause;
        }

        public void Init(Player player)
        {
            player.Died += OnPlayerDied;
            _player = player;
        }

        private void OnPlayerDied()
        {
            Finished?.Invoke();
            _gamePause.SetPause(true);
        }
    }
}