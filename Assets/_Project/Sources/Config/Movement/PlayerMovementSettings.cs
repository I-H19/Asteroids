using UnityEngine;

namespace Asteroids
{
    [CreateAssetMenu(fileName = "PlayerMovementsettings", menuName = "Settings/Movement/PlayerMovementSettings")]
    public class PlayerMovementSettings : ScriptableObject
    {
        [field: Header("Player movement settings")]
        [field: SerializeField]
        public InertialMoverSettings MoverSettings { get; private set; }


        [field: Header("Player rotation settings")]
        [field: SerializeField]
        public DirectionalRotationSettings RotationSettings { get; private set; }
    }
}