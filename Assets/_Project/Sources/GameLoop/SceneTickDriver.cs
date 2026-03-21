using _Project.Sources.Gameplay;
using _Project.Sources.Gameplay.EnemySystem.EnemySpawn;
using _Project.Sources.Gameplay.WeaponSystem;
using _Project.Sources.Input;
using VContainer;
using VContainer.Unity;

namespace _Project.Sources.GameLoop
{
    public class SceneTickDriver : ITickable
    {
        private KeyboardMonitor _keyboardMonitor;
        private PlayerKeyboardController _keyboardController;
        private BulletSpawner _bulletSpawner;
        private Laser _laser;
        private EnemyDriver _enemyDriver;
        private EnemySpawner _enemySpawner;
        private Player _player;
        private bool _isPaused;

        [Inject]
        public void Construct(EnemyDriver enemyTickDriver, PlayerKeyboardController keyboardController, 
            KeyboardMonitor keyboardMonitor, EnemySpawner enemySpawner)
        {
            _enemySpawner = enemySpawner;
            _keyboardMonitor = keyboardMonitor;
            _keyboardController = keyboardController;

            _enemyDriver = enemyTickDriver;
        }

        public void Init(Player player, BulletSpawner bulletSpawner)
        {
            _player = player;
            _laser = player.PlayerLaser;
            _bulletSpawner = bulletSpawner;
        }

        public void Tick()
        {
            if (!_isPaused)
            {
                _keyboardController.Tick();
                _keyboardMonitor.Tick();
                _bulletSpawner.Tick();
                _enemyDriver.Tick();
                _enemySpawner.Tick();
                _player.Tick();
                _laser.Tick();
            }
        }

        public void SetPause(bool isPaused)
        {
            _isPaused = isPaused;
        }
    }
}