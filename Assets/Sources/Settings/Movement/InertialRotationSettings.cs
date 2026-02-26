using System;
using UnityEngine;

[Serializable]
public class InertialRotationSettings
{
    [field: SerializeField] public float MaxAngularSpeed { get; private set; } = 200f;
    [field: SerializeField] public float AngularAcceleration { get; private set; } = 600f;
    [field: SerializeField] public float AngularDeceleration { get; private set; } = 400f;
    [field: SerializeField] public float AngularStopEpsilon { get; private set; } = 1f;

    public void ChangeValues(float maxAngularSpeed, float angularAcceleration, float angularDeceleration, float angularStopEpsilon)
    {
        MaxAngularSpeed = maxAngularSpeed;
        AngularAcceleration = angularAcceleration;
        AngularDeceleration = angularDeceleration;
        AngularStopEpsilon = angularStopEpsilon;
    }
}

