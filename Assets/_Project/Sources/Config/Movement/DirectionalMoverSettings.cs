using System;
using UnityEngine;

namespace _Project.Sources.Config.Movement
{
    [Serializable]
    public class DirectionalMoverSettings : IMoverSettings
    {
        [field: SerializeField] public float MovingSpeed { get; private set; }
    }
}

