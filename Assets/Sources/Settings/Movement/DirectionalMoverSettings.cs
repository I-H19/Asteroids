using System;
using UnityEngine;

[Serializable]
public class DirectionalMoverSettings : IMoverSettings
{
    [field: SerializeField] public float MovingSpeed { get; private set; }

    public void ChangeValues(float movingSpeed)
    {
        MovingSpeed = movingSpeed;
    }
}

