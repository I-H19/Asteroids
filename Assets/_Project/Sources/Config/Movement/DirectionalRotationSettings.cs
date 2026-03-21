using UnityEngine;

namespace _Project.Sources.Config.Movement
{
    [CreateAssetMenu(fileName = "DirectionalRotationSettings", menuName = "Settings/Movement/DirectionalRotationSettings")]
    public class DirectionalRotationSettings : ScriptableObject
    {
        [field: SerializeField] public float RotationSpeed { get; private set; }
        
    }
}

