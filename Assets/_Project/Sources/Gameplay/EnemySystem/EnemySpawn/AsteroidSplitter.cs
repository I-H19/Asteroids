using _Project.Sources.Gameplay.EnemySystem.Enemy;
using _Project.Sources.Gameplay.EnemySystem.EnemySpawn;
using VContainer;

public class AsteroidSplitter 
{
    private EnemyRegistry _enemyRegistry;
    
    [Inject]
    private void Construct(EnemyRegistry enemyRegistry) 
        => _enemyRegistry = enemyRegistry;

    public void Track(AsteroidFragment asteroidFragment) 
        => _enemyRegistry.RegisterEnemy(asteroidFragment);
}
