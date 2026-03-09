using System;
using UnityEngine;

namespace Asteroids.Settings
{
    [Serializable]
    public class DirectionalMoverSettings : IMoverSettings
    {
        [field: SerializeField] public float MovingSpeed { get; private set; }

        public void ChangeValues(float movingSpeed)
        {
            MovingSpeed = movingSpeed;
        }
    }
}

