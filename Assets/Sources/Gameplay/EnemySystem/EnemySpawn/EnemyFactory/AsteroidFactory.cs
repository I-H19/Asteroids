using UnityEngine;
using VContainer;
using VContainer.Unity;

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

        asteroid.Init(enemy, _enemySettings.AsteroidMovingSettings, _enemySettings.AsteroidDamage, false);

        return asteroid;
    }

    public Asteroid SpawnFragment(Vector3 position, Asteroid parent)
    {
        GameObject enemy = _resolver.Instantiate(_fragmentPrefab, position, Quaternion.identity);
        Asteroid fragment = enemy.GetComponent<Asteroid>();

        fragment.Init(enemy, _enemySettings.AsteroidMovingSettings, _enemySettings.AsteroidDamage, true);
        fragment.SetParent(parent);
        parent.AddFragment(enemy, _enemySettings.AsteroidMovingSettings, _enemySettings.AsteroidDamage);

        enemy.SetActive(false);

        return fragment;
    }
}
