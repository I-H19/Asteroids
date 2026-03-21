using UnityEngine;

namespace _Project.Sources.Config.Movement
{
    [CreateAssetMenu(fileName = "InertialRotationSettings", menuName = "Settings/Movement/InertialRotationSettings")]
    public class InertialRotationSettings : ScriptableObject
    {
        [field: SerializeField] public float MaxAngularSpeed { get; private set; } = 200f;
        [field: SerializeField] public float AngularAcceleration { get; private set; } = 600f;
        [field: SerializeField] public float AngularDeceleration { get; private set; } = 400f;
        [field: SerializeField] public float AngularStopEpsilon { get; private set; } = 1f;
        
    }
}
