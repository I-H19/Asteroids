using UnityEngine;

namespace Asteroids
{
    [CreateAssetMenu(fileName = "EnemyCombatSettings", menuName = "Settings/Combat/EnemyCombatSettings")]
    public class EnemyCombatSettings : ScriptableObject
    {
        [field: SerializeField]
        public float UfoDamage { get; private set; }

        [field: SerializeField]
        public float AsteroidDamage { get; private set; }

        [field: SerializeField]
        [field: Min(0)]
        public int AsteroidFragmentCount { get; private set; } = 3;

        [field: SerializeField]
        [field: Min(1)]
        public float DefaultMaxHealth { get; private set; } = 1f;
    }
}