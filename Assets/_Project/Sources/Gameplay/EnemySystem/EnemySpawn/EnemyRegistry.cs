using System;
using System.Collections.Generic;

namespace Asteroids
{
    public class EnemyRegistry : IDisposable
    {
        private readonly List<IEnemy> _aliveEnemies = new();

        public int AliveCount { get; private set; }

        public event Action EnemyKilled;

        public void Dispose()
        {
            Unsubscribe();
        }

        public void RegisterEnemy(IEnemy enemy)
        {
            enemy.Killed += OnEnemyKilled;

            _aliveEnemies.Add(enemy);
            AliveCount++;
        }

        public void KillAll()
        {
            foreach (IEnemy enemy in _aliveEnemies)
            {
                enemy.Kill();
            }

            _aliveEnemies.Clear();
            AliveCount = 0;
        }

        public void ForEachEnemy(Action<IEnemy> action)
        {
            foreach (IEnemy enemy in _aliveEnemies)
            {
                action(enemy);
            }
        }

        private void Unsubscribe()
        {
            foreach (IEnemy enemy in _aliveEnemies)
            {
                enemy.Killed -= OnEnemyKilled;
            }
        }

        private void OnEnemyKilled(IEnemy enemy)
        {
            AliveCount--;
            _aliveEnemies.Remove(enemy);

            enemy.Killed -= OnEnemyKilled;
            enemy.Kill();

            EnemyKilled?.Invoke();
        }
    }
}