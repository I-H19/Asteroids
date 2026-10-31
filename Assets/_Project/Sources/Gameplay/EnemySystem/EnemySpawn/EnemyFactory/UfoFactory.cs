using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Asteroids
{
    public class UfoFactory : IEnemyFactory
    {
        private Ufo _enemyPrefab;
        private EnemySettings _enemySettings;
        private IObjectResolver _resolver;

        public IEnemy SpawnOne(Vector3 position)
        {
            Ufo ufo = _resolver.Instantiate(_enemyPrefab, position, Quaternion.identity);

            ufo.Init(_enemySettings.UfoMovingSettings, _enemySettings.UfoDamage);

            return ufo;
        }

        [Inject]
        public void Construct(
            IObjectResolver resolver,
            PrefabHolder prefabHolder,
            EnemySettings enemySettings)
        {
            _resolver = resolver;
            _enemyPrefab = prefabHolder.UfoPrefab;
            _enemySettings = enemySettings;
        }
    }
}
