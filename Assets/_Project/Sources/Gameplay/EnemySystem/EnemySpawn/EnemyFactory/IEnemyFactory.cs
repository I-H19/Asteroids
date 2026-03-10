using _Project.Sources.Gameplay.EnemySystem.Enemy;
using UnityEngine;

namespace _Project.Sources.Gameplay.EnemySystem.EnemySpawn.EnemyFactory
{
    public interface IEnemyFactory
    {
        public IEnemy SpawnOne(Vector3 position);
    }
}