using _Project.Sources.Config.Movement;
using UnityEngine;

namespace _Project.Sources.Config.Gameplay
{
    [CreateAssetMenu(fileName = "EnemySettings", menuName = "Settings/Gameplay/EnemySettings")]
    public class EnemySettings : ScriptableObject
    {
        [field: SerializeField] public DirectionalMoverSettings AsteroidMovingSettings { get; private set; }
        [field: SerializeField] public DirectionalMoverSettings FragmentMovingSettings { get; private set; }
        [field: SerializeField] public DirectionalMoverSettings UfoMovingSettings { get; private set; }
        [field: SerializeField] public float UfoDamage { get; private set; }
        [field: SerializeField] public float AsteroidDamage { get; private set; }
        [field: SerializeField] public int AsteroidFragmentsNumber { get; private set; } = 3;
    }
}