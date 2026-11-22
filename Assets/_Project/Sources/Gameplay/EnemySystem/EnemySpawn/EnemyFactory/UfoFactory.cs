using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Asteroids
{
    public class UfoFactory : IEnemyFactory
    {
        private Ufo _enemyPrefab;
        private EnemyMovingSettings _enemyMovingSettings;
        private EnemyCombatSettings _enemyCombatSettings;
        private IObjectResolver _resolver;

        [Inject]
        public void Construct(
            IObjectResolver resolver,
            PrefabHolder prefabHolder,
            EnemyCombatSettings enemyCombatSettings,
            EnemyMovingSettings enemyMovingSettings)
        {
            _resolver = resolver;
            _enemyPrefab = prefabHolder.UfoPrefab;
            _enemyMovingSettings = enemyMovingSettings;
            _enemyCombatSettings = enemyCombatSettings;
        }

        public IEnemy SpawnOne(Vector3 position)
        {
            Ufo ufo = _resolver.Instantiate(_enemyPrefab, position, Quaternion.identity);

            ufo.Init(_enemyMovingSettings.UfoMovingSettings, _enemyCombatSettings.UfoDamage);

            return ufo;
        }
    }
}