using UnityEngine;

namespace Asteroids
{
    [CreateAssetMenu(fileName = "PrefabHolder", menuName = "Settings/PrefabHolder")]
    public class PrefabHolder : ScriptableObject
    {
        [Header("Gameplay")]
        [field: SerializeField]
        public Bullet BulletPrefab { get; private set; }

        [field: SerializeField]
        public Asteroid AsteroidPrefab { get; private set; }

        [field: SerializeField]
        public AsteroidFragment AsteroidFragmentPrefab { get; private set; }

        [field: SerializeField]
        public Ufo UfoPrefab { get; private set; }

        [field: Space]
        [Header("UI")]
        [field: SerializeField]
        public RootUI UI { get; private set; }

        [field: Space]
        [Header("Player")]
        [field: SerializeField]
        public Player PlayerRoot { get; private set; }
    }
}