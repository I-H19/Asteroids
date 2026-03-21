using UnityEngine;

namespace _Project.Sources.Config.Movement
{
    [CreateAssetMenu(fileName ="InertialMoverSettings", menuName = "Settings/Movement/InertialMoverSettings")]
    public class InertialMoverSettings :  ScriptableObject, IMoverSettings
    {
        [field: SerializeField] public float MaxForwardSpeed { get; private set; } = 200f;
        [field: SerializeField] public float ForwardAcceleration { get; private set; } = 600f;
        [field: SerializeField] public float CoastDeceleration { get; private set; } = 400f;
        [field: SerializeField] public float BrakeDeceleration { get; private set; } = 1f;
        [field: SerializeField] public float StopEpsilon { get; private set; } = 1f;
    }
}

