using System;
using UnityEngine;

namespace Asteroids
{
    [Serializable]
    public class DirectionalMoverSettings
    {
        [field: SerializeField]
        public float MovingSpeed { get; private set; }
    }
}