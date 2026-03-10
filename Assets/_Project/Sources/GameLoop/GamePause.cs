using _Project.Sources.Gameplay;
using _Project.Sources.Gameplay.EnemySystem.EnemySpawn;
using _Project.Sources.Gameplay.WeaponSystem;
using VContainer;

namespace _Project.Sources.GameLoop
{
    public class GamePause
    {
        private BulletSpawner _bulletSpawner;
        private EnemyDriver _enemyDriver;
        private SceneTickDriver _sceneTickDriver;
        private Player _player;

        [Inject]
        public void Construct(SceneTickDriver sceneTickDriver, EnemyDriver enemyTickDriver, Player player, BulletSpawner bulletSpawner)
        {
            _bulletSpawner = bulletSpawner;
            _enemyDriver = enemyTickDriver;
            _sceneTickDriver = sceneTickDriver;
            _player = player;
        }
        public void SetPause(bool pause)
        {
            _sceneTickDriver.SetPause(pause);

            if (pause)
            {
                _enemyDriver.FreezeEnemyMoving();
                _player.InertialMoverTemplate.SetEnabled(false);
                _player.DirectionalRotatorTemplate.SetEnabled(false);
                _bulletSpawner.StopSpawning();
            }
            else
            {
                _enemyDriver.UnfreezeEnemyMoving();
                _player.InertialMoverTemplate.SetEnabled(true);
                _player.DirectionalRotatorTemplate.SetEnabled(true);
            }
        }
    }
}