using UnityEngine;

namespace Asteroids.Settings
{
    public class EnemySpawnerSettings : MonoBehaviour
    {
        [field: SerializeField] public float AsteroidSpawnChancePerSecond { get; private set; }
        [field: SerializeField] public float UfoSpawnChancePerSecond { get; private set; }
        [field: SerializeField] public int MaxEnemiesAlive { get; private set; }
    }
}
