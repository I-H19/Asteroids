using VContainer;

namespace Asteroids
{
    public class AsteroidSplitter
    {
        private EnemyRegistry _enemyRegistry;

        [Inject]
        private void Construct(EnemyRegistry enemyRegistry)
        {
            _enemyRegistry = enemyRegistry;
        }

        public void Track(AsteroidFragment asteroidFragment)
        {
            _enemyRegistry.RegisterEnemy(asteroidFragment);
        }
    }
}