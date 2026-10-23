using UnityEngine;
using UnityEngine.Serialization;

namespace Asteroids
{
    [CreateAssetMenu(fileName = "EnemySpawnerSettings", menuName = "Settings/Gameplay/EnemySpawnerSettings")]
    public class EnemySpawnerSettings : ScriptableObject
    {
        [field: SerializeField]
        [field: Range(1, 100)]
        [field: FormerlySerializedAs("<SpawnTryingInterval>k__BackingField")]
        public float SpawnAttemptInterval { get; private set; }

        [field: SerializeField]
        [field: Range(0, 100)]
        public byte EnemySpawnChance { get; private set; }

        [field: SerializeField]
        [field: Range(0, 100)]
        public byte AsteroidSpawnChance { get; private set; }

        [field: SerializeField]
        [field: Range(0, 100)]
        public byte UfoSpawnChance { get; private set; }

        [field: SerializeField]
        [field: Range(0, 255)]
        public byte MaxEnemiesAlive { get; private set; }
    }
}
