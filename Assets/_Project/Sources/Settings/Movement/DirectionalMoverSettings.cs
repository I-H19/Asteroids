using System;
using UnityEngine;

namespace _Project.Sources.Settings.Movement
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

