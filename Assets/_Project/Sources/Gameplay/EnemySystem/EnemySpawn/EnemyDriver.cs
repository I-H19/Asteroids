using _Project.Sources.GameLoop;
using VContainer;

namespace _Project.Sources.Gameplay.EnemySystem.EnemySpawn
{
    public class EnemyDriver : ISceneTickable, IPausable
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
                enemy.Mover.DisableMoving();
            });
        }

        public void UnfreezeEnemyMoving()
        {
            _enemiesRegistry.ForEachEnemy(enemy =>
            {
                enemy.Mover.EnableMoving();
            });
        }

        public void Pause() => FreezeEnemyMoving();
        public void Unpause() => UnfreezeEnemyMoving();
    }
}