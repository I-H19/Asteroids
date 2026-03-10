using System;
using UnityEngine;

namespace _Project.Sources.Settings.Movement
{
    [Serializable]
    public class DirectionalRotationSettings
    {
        [field: SerializeField] public float RotationSpeed { get; private set; }

        public void ChangeValues(float rotationSpeed)
        {
            RotationSpeed = rotationSpeed;
        }
    }
}

