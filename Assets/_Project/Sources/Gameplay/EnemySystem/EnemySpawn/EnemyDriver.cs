using _Project.Sources.GameLoop;
using _Project.Sources.Gameplay.EnemySystem.Enemy;
using VContainer;

namespace _Project.Sources.Gameplay.EnemySystem.EnemySpawn
{
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
            _enemiesRegistry.ForEachEnemy(enemy =>
            {
                enemy.Tick();
            });
        }

        public void FreezeEnemyMoving()
        {
            _enemiesRegistry.ForEachEnemy(enemy =>
            {
                enemy.Mover.SetEnabled(false);
            });
        }

        public void UnfreezeEnemyMoving()
        {
            _enemiesRegistry.ForEachEnemy(enemy =>
            {
                enemy.Mover.SetEnabled(true);
            });
        }

    }
}