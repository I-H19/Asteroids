using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Asteroids
{
    public class AsteroidFactory : IEnemyFactory
    {
        private Asteroid _asteroidPrefab;
        private AsteroidSplitter _asteroidSplitter;
        private EnemySettings _enemySettings;
        private AsteroidFragment _fragmentPrefab;
        private IObjectResolver _resolver;

        [Inject]
        public void Construct(
            IObjectResolver resolver,
            PrefabHolder prefabHolder,
            EnemySettings enemySettings,
            AsteroidSplitter asteroidSplitter)
        {
            _resolver = resolver;
            _asteroidPrefab = prefabHolder.AsteroidPrefab;
            _fragmentPrefab = prefabHolder.AsteroidFragmentPrefab;
            _enemySettings = enemySettings;
            _asteroidSplitter = asteroidSplitter;
        }

        public IEnemy SpawnOne(Vector3 position)
        {
            Asteroid asteroid = _resolver.Instantiate(_asteroidPrefab, position, Quaternion.identity);

            asteroid.Init(_enemySettings.AsteroidMovingSettings, _enemySettings.AsteroidDamage);

            for (int i = 0; i < _enemySettings.AsteroidFragmentCount; i++)
            {
                AsteroidFragment fragment = CreateFragment(position, asteroid);
                _asteroidSplitter.Track(fragment);
            }

            return asteroid;
        }

        private AsteroidFragment CreateFragment(Vector3 position, Asteroid parent)
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