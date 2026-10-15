using System;
using UnityEngine;

namespace _Project.Sources.Config.Movement
{
    [Serializable]
    public class DirectionalMoverSettings
    {
        [field: SerializeField] public float MovingSpeed { get; private set; }
    }
}

