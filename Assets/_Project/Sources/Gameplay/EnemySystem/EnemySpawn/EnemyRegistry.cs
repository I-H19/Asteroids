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
        public Action ScoredKilled;
        public readonly List<IEnemy> AliveEnemies = new();

        public int AliveCount { get; private set; }

        public void RegisterEnemy(IEnemy enemy)
        {
            enemy.Killed += ScoredKill;
            
            AliveEnemies.Add(enemy);
            AliveCount++;
        }

        public void KillAll()
        {
            foreach (IEnemy enemy in AliveEnemies)
            {
                if (enemy != null)
                {
                    Component enemyComponent = enemy as Component;
                    if (enemyComponent != null) enemy.Kill();
                    else throw new NullReferenceException($"enemyComponent is null");
                }
            }

            AliveEnemies.Clear();
            AliveCount = 0;
        }

        private void OnDestroy() => Unsubscribe();

        private void Unsubscribe()
        {
            foreach (IEnemy enemy in AliveEnemies)
            {
                enemy.Killed -= ScoredKill;
            }
        }

        private void ScoredKill(IEnemy enemy)
        {
            if (enemy is Asteroid asteroid)
            {
                if (asteroid.IsFragment && !asteroid.IsActiveFragment) return;
                if (!asteroid.IsFragment) asteroid.ActivateFragments();
            }

            AliveCount--;
            AliveEnemies.Remove(enemy);

            enemy.Killed -= ScoredKill;
            enemy.Kill();

            ScoredKilled?.Invoke();
        }

        public void Dispose() => Unsubscribe();
    }
}