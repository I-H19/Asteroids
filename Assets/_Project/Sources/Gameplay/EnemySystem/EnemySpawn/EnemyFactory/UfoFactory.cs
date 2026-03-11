using _Project.Sources.Gameplay.EnemySystem.Enemy;
using _Project.Sources.Settings;
using _Project.Sources.Settings.Gameplay;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Sources.Gameplay.EnemySystem.EnemySpawn.EnemyFactory
{
    public class UfoFactory : IEnemyFactory
    {
        private IObjectResolver _resolver;
        private GameObject _enemyPrefab;
        private EnemySettings _enemySettings;
        private Player _player;

        [Inject]
        public void Construct(IObjectResolver resolver, PrefabHolder prefabHolder, EnemySettings enemySettings, Player player)
        {
            _resolver = resolver;
            _enemyPrefab = prefabHolder.UFO;
            _enemySettings = enemySettings;
            _player = player;
        }
        public IEnemy SpawnOne(Vector3 position)
        {
            GameObject enemy = _resolver.Instantiate(_enemyPrefab, position, Quaternion.identity);
            UFO ufo = enemy.GetComponent<UFO>();

            ufo.Init(_enemySettings.UfoMovingSettings, _player, _enemySettings.UfoDamage);

            return ufo;
        }
    }
}