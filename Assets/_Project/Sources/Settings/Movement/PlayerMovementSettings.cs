using UnityEngine;

public class PlayerMovementSettings : MonoBehaviour
{
    [field: Header("Player movement settings")]
    [field: SerializeField] public InertialMoverSettings MoverSettings { get; private set; }


    [field: Header("Player rotation settings")]
    [field: SerializeField] public DirectionalRotationSettings RotationSettings { get; private set; }
}
