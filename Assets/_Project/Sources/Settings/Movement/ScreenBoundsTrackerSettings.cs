using UnityEngine;

namespace Asteroids.Settings
{
    public class ScreenBoundsTrackerSettings : MonoBehaviour
    {
        [field: SerializeField] public float PlayerBoundsMultiplier { get; private set; } = 1f;
        [field: SerializeField] public float EnemyBoundsMultiplier { get; private set; } = 2f;
    }
}
