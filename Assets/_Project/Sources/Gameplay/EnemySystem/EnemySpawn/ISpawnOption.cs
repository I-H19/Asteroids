using _Project.Sources.Gameplay.EnemySystem.EnemySpawn.EnemyFactory;

public class SpawnOption
{
    public int Rate { get; }
    public IEnemyFactory Factory { get; }

    public SpawnOption(int rate, IEnemyFactory enemyFactory)
    {
        Rate = rate;
        Factory = enemyFactory;
    }
}