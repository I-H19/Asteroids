using System;
using _Project.Sources.Gameplay;
using _Project.Sources.Gameplay.EnemySystem.EnemySpawn;
using UnityEngine.UI;
using VContainer;

namespace _Project.Sources.GameLoop
{
    public class GameRestarter : IDisposable
    {
        public event Action Restarted;

        private GamePause _gamePause;
        private EnemyRegistry _enemiesRegistry;
        private Player _player;
        private Button _gameRestartButton;


        [Inject]
        public void Construct(EnemyRegistry enemiesRegistry, GamePause gamePause)
        {
            _gamePause = gamePause;
            _enemiesRegistry = enemiesRegistry;
        }

        public void Init(Player player, Button restartButton)
        {
            _player = player;
            _gameRestartButton =  restartButton;
            
            _gameRestartButton.onClick.AddListener(RestartGame);
        }

        public void RestartGame()
        {
            _enemiesRegistry.KillAll();
            _player.ResetPlayer();

            Restarted?.Invoke();

            _gamePause.SetPause(false);
        }

        public void Dispose() => _gameRestartButton.onClick.RemoveAllListeners();
    }
}