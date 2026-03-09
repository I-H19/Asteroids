using UnityEngine;

namespace Asteroids.Gameplay.EnemySystem
{
    public interface IEnemyFactory
    {
        public IEnemy SpawnOne(Vector3 position);
    }
}