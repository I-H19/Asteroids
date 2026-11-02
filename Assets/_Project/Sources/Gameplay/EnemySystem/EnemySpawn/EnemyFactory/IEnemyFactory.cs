using UnityEngine;

namespace Asteroids
{
    public interface IEnemyFactory
    {
        public IEnemy SpawnOne(Vector3 position);
    }
}