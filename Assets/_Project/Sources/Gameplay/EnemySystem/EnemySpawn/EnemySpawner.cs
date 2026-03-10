using _Project.Sources.GameLoop;
using _Project.Sources.Gameplay.EnemySystem.Enemy;
using _Project.Sources.Gameplay.EnemySystem.EnemySpawn.EnemyFactory;
using _Project.Sources.Gameplay.ObjectMovement;
using _Project.Sources.Settings.Gameplay;
using _Project.Sources.Settings.Movement;
using UnityEngine;
using VContainer;

namespace _Project.Sources.Gameplay.EnemySystem.EnemySpawn
{
    [RequireComponent(typeof(ScreenBoundsTracker))]
    public class EnemySpawner : MonoBehaviour, ISceneTickable
    {
        private EnemyRegistry _enemiesRegistry;

        private EnemySpawnerSettings _enemySpawnerSettings;
        private EnemySettings _enemySettings;
        private IObjectResolver _resolver;
        private ScreenBoundsTracker _boundsTracker;

        private AsteroidFactory _asteroidFactory;
        private UfoFactory _ufoFactory;


        [Inject]
        public void Construct(IObjectResolver resolver, EnemySpawnerSettings enemySpawnerSettings,
            ScreenBoundsTrackerSettings screenBoundsTrackerSettings, AsteroidFactory asteroidFactory,
            UfoFactory ufoFactory, EnemyRegistry enemiesRegistry, EnemySettings enemySettings)
        {
            _enemiesRegistry = enemiesRegistry;

            _enemySpawnerSettings = enemySpawnerSettings;
            _enemySettings = enemySettings;

            _resolver = resolver;

            _boundsTracker = GetComponent<ScreenBoundsTracker>();
            _resolver.Inject(_boundsTracker);
            _boundsTracker.ChangeTrackingBounds(screenBoundsTrackerSettings.EnemyBoundsMultiplier);

            _asteroidFactory = asteroidFactory;
            _ufoFactory = ufoFactory;
        }
        public void Tick()
        {
            if (_enemySpawnerSettings.MaxEnemiesAlive > 0 &&
                _enemiesRegistry.AliveCount >= _enemySpawnerSettings.MaxEnemiesAlive)
            {
                return;
            }

            float deltaTime = Time.deltaTime;
            if (deltaTime <= 0f)
            {
                return;
            }

            float asteroidSpawnChancePerSecond = Mathf.Max(0f, _enemySpawnerSettings.AsteroidSpawnChancePerSecond);
            float ufoSpawnChancePerSecond = Mathf.Max(0f, _enemySpawnerSettings.UfoSpawnChancePerSecond);
            float totalSpawnChancePerSecond = asteroidSpawnChancePerSecond + ufoSpawnChancePerSecond;

            if (totalSpawnChancePerSecond <= 0f)
            {
                return;
            }

            float probabilityToSpawnThisTick = 1f - Mathf.Exp(-totalSpawnChancePerSecond * deltaTime);
            if (Random.value >= probabilityToSpawnThisTick)
            {
                return;
            }

            bool shouldSpawnAsteroid = ChooseAsteroid(asteroidSpawnChancePerSecond, totalSpawnChancePerSecond);
            Vector3 spawnPosition = _boundsTracker.GetRandomPointOnPerimeter();

            if (shouldSpawnAsteroid)
            {
                IEnemy parentAsteroid = _asteroidFactory.SpawnOne(spawnPosition);
                _enemiesRegistry.RegisterEnemy(parentAsteroid);

                for (int i = 0; i != _enemySettings.AsteroidFragmentsNumber; i++)
                {
                    IEnemy fragment = _asteroidFactory.SpawnFragment(spawnPosition, (Asteroid)parentAsteroid);
                    _enemiesRegistry.RegisterEnemy(fragment);
                }
            }
            else
            {
                IEnemy enemy = _ufoFactory.SpawnOne(spawnPosition);
                _enemiesRegistry.RegisterEnemy(enemy);
            }

        }

        private bool ChooseAsteroid(float asteroidSpawnChancePerSecond, float totalSpawnChancePerSecond)
        {
            if (asteroidSpawnChancePerSecond <= 0f)
            {
                return false;
            }

            float randomValue = Random.value * totalSpawnChancePerSecond;
            return randomValue < asteroidSpawnChancePerSecond;
        }
    }
}