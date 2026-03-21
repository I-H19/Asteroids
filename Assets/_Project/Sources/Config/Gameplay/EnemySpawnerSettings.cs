using UnityEngine;

namespace _Project.Sources.Config.Gameplay
{
    [CreateAssetMenu(fileName = "EnemySpawnerSettings", menuName = "Settings/Gameplay/EnemySpawnerSettings")]
    public class EnemySpawnerSettings : ScriptableObject
    {
        [field: SerializeField] public float AsteroidSpawnChancePerSecond { get; private set; }
        [field: SerializeField] public float UfoSpawnChancePerSecond { get; private set; }
        [field: SerializeField] public int MaxEnemiesAlive { get; private set; }
    }
}
