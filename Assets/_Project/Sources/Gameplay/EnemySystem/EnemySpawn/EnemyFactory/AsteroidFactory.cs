using _Project.Sources.Gameplay.EnemySystem.Enemy;
using _Project.Sources.Settings;
using _Project.Sources.Settings.Gameplay;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Sources.Gameplay.EnemySystem.EnemySpawn.EnemyFactory
{
    public class AsteroidFactory : IEnemyFactory
    {
        private IObjectResolver _resolver;
        private GameObject _asteroidPrefab;
        private GameObject _fragmentPrefab;
        private EnemySettings _enemySettings;

        [Inject]
        public void Construct(IObjectResolver resolver, PrefabHolder prefabHolder, EnemySettings enemySettings)
        {
            _resolver = resolver;
            _asteroidPrefab = prefabHolder.Asteroid;
            _fragmentPrefab = prefabHolder.AsteroidFragment;
            _enemySettings = enemySettings;
        }
        public IEnemy SpawnOne(Vector3 position)
        {
            GameObject enemy = _resolver.Instantiate(_asteroidPrefab, position, Quaternion.identity);
            Asteroid asteroid = enemy.GetComponent<Asteroid>();

            asteroid.Init(_enemySettings.AsteroidMovingSettings, _enemySettings.AsteroidDamage, false);

            return asteroid;
        }

        public AsteroidFragment SpawnFragment(Vector3 position, Asteroid parent)
        {
            GameObject enemy = _resolver.Instantiate(_fragmentPrefab, position, Quaternion.identity);
            AsteroidFragment fragment = enemy.GetComponent<AsteroidFragment>();

            fragment.Init(_enemySettings.AsteroidMovingSettings, _enemySettings.AsteroidDamage);
            fragment.SetParent(parent);
            parent.AddFragment(enemy);

            enemy.SetActive(false);

            return fragment;
        }
    }
}