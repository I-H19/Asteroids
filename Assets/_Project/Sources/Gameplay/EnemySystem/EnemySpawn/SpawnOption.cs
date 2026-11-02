namespace Asteroids
{
    public class SpawnOption
    {
        public int Weight { get; }
        public IEnemyFactory Factory { get; }

        public SpawnOption(int weight, IEnemyFactory enemyFactory)
        {
            Weight = weight;
            Factory = enemyFactory;
        }
    }
}