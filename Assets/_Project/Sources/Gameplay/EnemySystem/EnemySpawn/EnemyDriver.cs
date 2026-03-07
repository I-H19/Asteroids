using VContainer;

public class EnemyDriver : ISceneTickable
{
    private EnemyRegistry _enemiesRegistry;

    private bool _isEnemiesTicking = true;

    [Inject]
    public void Construct(EnemyRegistry enemiesRegistry)
    {
        _enemiesRegistry = enemiesRegistry;

        _isEnemiesTicking = true;
    }
    public void Tick()
    {
        if (!_isEnemiesTicking) return;

        foreach (IEnemy enemy in _enemiesRegistry.AliveEnemies)
        {
            enemy.Tick();
        }
    }

    public void FreezeEnemyMoving()
    {
        foreach (IEnemy enemy in _enemiesRegistry.AliveEnemies)
        {
            enemy.Mover.SetEnabled(false);
        }
    }

    public void UnfreezeEnemyMoving()
    {
        foreach (IEnemy enemy in _enemiesRegistry.AliveEnemies)
        {
            enemy.Mover.SetEnabled(true);
        }
    }

}