namespace Asteroids
{
    public class SpawnOption
    {
        public SpawnOption(int weight, IEnemyFactory enemyFactory)
        {
            Weight = weight;
            Factory = enemyFactory;
        }

        public int Weight { get; }
        public IEnemyFactory Factory { get; }
    }
}