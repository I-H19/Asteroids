using UnityEngine;

namespace Asteroids
{
    [CreateAssetMenu(fileName = "EnemyMovingSettings", menuName = "Settings/Movement/EnemyMovingSettings")]
    public class EnemyMovingSettings : ScriptableObject
    {
        [field: SerializeField]
        public DirectionalMoverSettings AsteroidMovingSettings { get; private set; }

        [field: SerializeField]
        public DirectionalMoverSettings FragmentMovingSettings { get; private set; }

        [field: SerializeField]
        public DirectionalMoverSettings UfoMovingSettings { get; private set; }
    }
}