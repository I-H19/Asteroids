using UnityEngine;
using VContainer;
using VContainer.Unity;

public class AsteroidFactory : IEnemyFactory
{
    private IObjectResolver _resolver;
    private GameObject _enemyPrefab;
    private EnemySettings _enemySettings;

    [Inject]
    public void Construct(IObjectResolver resolver, PrefabHolder prefabHolder, EnemySettings enemySettings)
    {
        _resolver = resolver;
        _enemyPrefab = prefabHolder.Asteroid;
        _enemySettings = enemySettings;
    }
    public IEnemy SpawnOne(Vector3 position)
    {
        GameObject enemy = _resolver.Instantiate(_enemyPrefab, position, Quaternion.identity);
        Asteroid asteroid = enemy.GetComponent<Asteroid>();
        asteroid.Init(enemy, _enemySettings.AsteroidMovingSettings, _enemySettings.AsteroidDamage);

        return asteroid;
    }
}
