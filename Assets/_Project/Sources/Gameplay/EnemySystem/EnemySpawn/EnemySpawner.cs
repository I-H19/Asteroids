using UnityEngine;
using VContainer;

namespace Asteroids
{
    public class EnemySpawner : ISceneTickable
    {
        private ScreenBoundsTracker _boundsTracker;
        private EnemyRegistry _enemiesRegistry;
        private EnemySpawnerSettings _enemySpawnerSettings;

        private SpawnOptionsTable _spawnOptionsTable;

        private float _spawnTimer;

        [Inject]
        public void Construct(
            EnemySpawnerSettings enemySpawnerSettings,
            EnemyRegistry enemiesRegistry,
            ScreenBoundsTracker boundsTracker,
            SpawnOptionsTable spawnOptionsTable)
        {
            _enemiesRegistry = enemiesRegistry;

            _enemySpawnerSettings = enemySpawnerSettings;

            _boundsTracker = boundsTracker;
            _boundsTracker.InitBounds();

            _spawnOptionsTable = spawnOptionsTable;
        }

        public void Tick()
        {
            _spawnTimer += Time.deltaTime;

            if (_spawnTimer < _enemySpawnerSettings.SpawnAttemptInterval)
            {
                return;
            }

            _spawnTimer -= _enemySpawnerSettings.SpawnAttemptInterval;

            if (_enemiesRegistry.AliveCount >= _enemySpawnerSettings.MaxEnemiesAlive)
            {
                return;
            }

            if (Random.Range(0, 100) >= _enemySpawnerSettings.EnemySpawnChance)
            {
                return;
            }

            Vector3 spawnPosition = _boundsTracker.GetRandomPointOnPerimeter();
            IEnemy enemy = _spawnOptionsTable.SpawnRandom(spawnPosition);

            if (enemy == null)
            {
                return;
            }

            _enemiesRegistry.RegisterEnemy(enemy);
        }
    }
}