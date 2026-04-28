using System;
using System.Collections.Generic;
using _Project.Sources.Gameplay.EnemySystem.Enemy;
using _Project.Sources.UI;
using UnityEngine;
using VContainer;

namespace _Project.Sources.Gameplay.EnemySystem.EnemySpawn
{
    public class EnemyRegistry : IDisposable
    {
        public event Action ScoredKilled;
        private readonly List<IEnemy> _aliveEnemies = new();

        public int AliveCount { get; private set; }

        public void RegisterEnemy(IEnemy enemy)
        {
            enemy.Killed += ScoredKill;

            _aliveEnemies.Add(enemy);
            AliveCount++;
        }

        public void KillAll()
        {
            foreach (IEnemy enemy in _aliveEnemies)
            {
                if (enemy != null)
                    enemy.Kill();
                else throw new NullReferenceException($"enemy is null");
            }

            _aliveEnemies.Clear();
            AliveCount = 0;
        }

        private void OnDestroy() => Unsubscribe();

        private void Unsubscribe()
        {
            foreach (IEnemy enemy in _aliveEnemies)
            {
                enemy.Killed -= ScoredKill;
            }
        }

        private void ScoredKill(IEnemy enemy)
        {
            AliveCount--;
            _aliveEnemies.Remove(enemy);

            enemy.Killed -= ScoredKill;
            enemy.Kill();

            ScoredKilled?.Invoke();
        }

        public void ForEachEnemy(Action<IEnemy> action)
        {
            foreach (IEnemy enemy in _aliveEnemies)
            {
                action(enemy);
            }
        }

        public void Dispose() => Unsubscribe();
    }
}