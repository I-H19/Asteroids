using UnityEngine;

public class EnemySettings : MonoBehaviour
{
    [field: SerializeField] public DirectionalMoverSettings AsteroidMovingSettings { get; private set; }
    [field: SerializeField] public DirectionalMoverSettings UfoMovingSettings { get; private set; }
    [field: SerializeField] public float UfoDamage { get; private set; }
    [field: SerializeField] public float AsteroidDamage { get; private set; }
    [field: SerializeField] public int AsteroidFragmentsNumber { get; private set; } = 3;

    

}
