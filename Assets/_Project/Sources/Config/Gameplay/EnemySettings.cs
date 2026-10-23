using UnityEngine;
using UnityEngine.Serialization;

namespace Asteroids
{
    [CreateAssetMenu(fileName = "EnemySettings", menuName = "Settings/Gameplay/EnemySettings")]
    public class EnemySettings : ScriptableObject
    {
        [field: SerializeField]
        public DirectionalMoverSettings AsteroidMovingSettings { get; private set; }

        [field: SerializeField]
        public DirectionalMoverSettings FragmentMovingSettings { get; private set; }

        [field: SerializeField]
        public DirectionalMoverSettings UfoMovingSettings { get; private set; }

        [field: SerializeField]
        public float UfoDamage { get; private set; }

        [field: SerializeField]
        public float AsteroidDamage { get; private set; }

        [field: SerializeField]
        [field: FormerlySerializedAs("<AsteroidFragmentsNumber>k__BackingField")]
        public int AsteroidFragmentCount { get; private set; } = 3;
    }
}
