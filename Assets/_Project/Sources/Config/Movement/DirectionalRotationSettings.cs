using UnityEngine;

namespace Asteroids
{
    [CreateAssetMenu(
        fileName = "DirectionalRotationSettings",
        menuName = "Settings/Movement/DirectionalRotationSettings")]
    public class DirectionalRotationSettings : ScriptableObject
    {
        [field: SerializeField]
        public float RotationSpeed { get; private set; }
    }
}