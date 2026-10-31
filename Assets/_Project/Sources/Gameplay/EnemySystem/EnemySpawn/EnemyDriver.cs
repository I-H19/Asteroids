using VContainer;

namespace Asteroids
{
    public class EnemyDriver : ISceneFixedTickable, IPausable
    {
        private EnemyRegistry _enemiesRegistry;

        private bool _isEnemiesTicking = true;

        public void Pause()
        {
            FreezeEnemyMoving();
        }

        public void Unpause()
        {
            UnfreezeEnemyMoving();
        }

        public void FixedTick()
        {
            if (!_isEnemiesTicking)
            {
                return;
            }

            _enemiesRegistry.ForEachEnemy(enemy =>
            {
                enemy.FixedTick();
            });
        }

        [Inject]
        public void Construct(EnemyRegistry enemiesRegistry)
        {
            _enemiesRegistry = enemiesRegistry;

            _isEnemiesTicking = true;
        }

        private void FreezeEnemyMoving()
        {
            _isEnemiesTicking = false;
            
            _enemiesRegistry.ForEachEnemy(enemy =>
            {
                enemy.Mover.DisableMoving();
            });
        }

        private void UnfreezeEnemyMoving()
        {
            _isEnemiesTicking = true;
            
            _enemiesRegistry.ForEachEnemy(enemy =>
            {
                enemy.Mover.EnableMoving();
            });
        }
    }
}
