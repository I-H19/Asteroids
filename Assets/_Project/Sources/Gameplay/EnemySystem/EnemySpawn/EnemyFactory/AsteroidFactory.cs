using _Project.Sources.Config;
using _Project.Sources.Config.Gameplay;
using _Project.Sources.Gameplay.EnemySystem.Enemy;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Sources.Gameplay.EnemySystem.EnemySpawn.EnemyFactory
{
    public class AsteroidFactory : IEnemyFactory
    {
        private IObjectResolver _resolver;
        private Asteroid _asteroidPrefab;
        private AsteroidFragment _fragmentPrefab;
        private EnemySettings _enemySettings;

        [Inject]
        public void Construct(IObjectResolver resolver, PrefabHolder prefabHolder, EnemySettings enemySettings)
        {
            _resolver = resolver;
            _asteroidPrefab = prefabHolder.AsteroidTemplate;
            _fragmentPrefab = prefabHolder.AsteroidFragmentTemplate;
            _enemySettings = enemySettings;
        }
        public IEnemy SpawnOne(Vector3 position)
        {
            Asteroid asteroid = _resolver.Instantiate(_asteroidPrefab, position, Quaternion.identity);

            asteroid.Init(_enemySettings.AsteroidMovingSettings, _enemySettings.AsteroidDamage, false);

            return asteroid;
        }

        public AsteroidFragment SpawnFragment(Vector3 position, Asteroid parent)
        {
            AsteroidFragment fragment = _resolver.Instantiate(_fragmentPrefab, position, Quaternion.identity);

            fragment.Init(_enemySettings.FragmentMovingSettings, _enemySettings.AsteroidDamage);
            fragment.SetParent(parent);
            parent.AddFragment(fragment);

            fragment.gameObject.SetActive(false);

            return fragment;
        }
    }
}