using System;
using _Project.Sources.Gameplay;
using _Project.Sources.Gameplay.EnemySystem.EnemySpawn;
using _Project.Sources.Scopes;
using _Project.Sources.UI;
using VContainer;

namespace _Project.Sources.GameLoop
{
    public class GameRestarter
    {
        public event Action Restarted;

        private GamePause _gamePause;
        private EnemyRegistry _enemiesRegistry;
        private Player _player;


        [Inject]
        public void Construct(EnemyRegistry enemiesRegistry, GamePause gamePause)
        {
            _gamePause = gamePause;
            _enemiesRegistry = enemiesRegistry;
        }

        public void Init(Player player) => _player = player;

        public void RestartGame()
        {
            _enemiesRegistry.KillAll();
            _player.ResetPlayer();

            Restarted?.Invoke();

            _gamePause.SetPause(false);
        }
    }
}