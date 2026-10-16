using UnityEngine;

namespace _Project.Sources.Config.Gameplay
{
    [CreateAssetMenu(fileName = "EnemySpawnerSettings", menuName = "Settings/Gameplay/EnemySpawnerSettings")]
    public class EnemySpawnerSettings : ScriptableObject
    {
        [field: SerializeField, Range(1, 100)] public float SpawnTryingInterval { get; private set; }
        [field: SerializeField, Range(0, 100)] public byte EnemySpawnChance { get; private set; }
        [field: SerializeField, Range(0, 100)] public byte AsteroidSpawnChance { get; private set; }
        [field: SerializeField, Range(0, 100)] public byte UfoSpawnChance { get; private set; }
        [field: SerializeField, Range(0, 255)] public byte MaxEnemiesAlive { get; private set; }
    }
}
