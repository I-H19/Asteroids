using System;
using UnityEngine;

[Serializable]
public class InertialMoverSettings
{
    [field: SerializeField] public float MaxForwardSpeed { get; private set; } = 200f;
    [field: SerializeField] public float ForwardAcceleration { get; private set; } = 600f;
    [field: SerializeField] public float CoastDeceleration { get; private set; } = 400f;
    [field: SerializeField] public float BrakeDeceleration { get; private set; } = 1f;
    [field: SerializeField] public float StopEpsilon { get; private set; } = 1f;


    public void ChangeValues(float maxForwardSpeed, float forwardAcceleration, float coastDeceleration, float brakeDeceleration, float stopEpsilon)
    {
        MaxForwardSpeed = maxForwardSpeed;
        ForwardAcceleration = forwardAcceleration;
        CoastDeceleration = coastDeceleration;
        BrakeDeceleration = brakeDeceleration;
        StopEpsilon = stopEpsilon;
    }
}

