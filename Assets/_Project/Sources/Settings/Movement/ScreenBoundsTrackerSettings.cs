using UnityEngine;

namespace _Project.Sources.Settings.Movement
{
    public class ScreenBoundsTrackerSettings : MonoBehaviour
    {
        [field: SerializeField] public float PlayerBoundsMultiplier { get; private set; } = 1f;
        [field: SerializeField] public float EnemyBoundsMultiplier { get; private set; } = 2f;
    }
}
