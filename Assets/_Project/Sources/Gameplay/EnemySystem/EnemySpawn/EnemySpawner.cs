using _Project.Sources.Config.Gameplay;
using _Project.Sources.GameLoop;
using _Project.Sources.Gameplay.EnemySystem.Enemy;
using _Project.Sources.Gameplay.ObjectMovement;
using UnityEngine;
using VContainer;

namespace _Project.Sources.Gameplay.EnemySystem.EnemySpawn
{
    public class EnemySpawner : ISceneTickable
    {
        private EnemyRegistry _enemiesRegistry;

        private SpawnOptionsTable _spawnOptionsTable;
        private EnemySpawnerSettings _enemySpawnerSettings;

        private ScreenBoundsTracker _boundsTracker;

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

            if (_spawnTimer < _enemySpawnerSettings.SpawnTryingInterval)
                return;

            _spawnTimer -= _enemySpawnerSettings.SpawnTryingInterval;
            
            if(_enemiesRegistry.AliveCount >= _enemySpawnerSettings.MaxEnemiesAlive)
                return;
            
            if (Random.Range(0, 100) >= _enemySpawnerSettings.EnemySpawnChance)
                return;

            Vector3 spawnPosition = _boundsTracker.GetRandomPointOnPerimeter();
            IEnemy enemy = _spawnOptionsTable.SpawnOneByChance(spawnPosition);
            
            if (enemy == null) 
                return;
            
            _enemiesRegistry.RegisterEnemy(enemy);
        }
        
    }
}
