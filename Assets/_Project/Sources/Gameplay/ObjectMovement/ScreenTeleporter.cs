using UnityEngine;

namespace Asteroids
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class ScreenTeleporter : MonoBehaviour
    {
        private ScreenBoundsTracker _boundsTracker;
        private Rigidbody2D _rigidbody;

        public void Init(ScreenBoundsTracker tracker)
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _boundsTracker = tracker;
        }

        public void FixedTick()
        {
            if (!_boundsTracker.IsOutOfBounds())
            {
                return;
            }

            _rigidbody.position = _boundsTracker.GetTeleportPosition();
        }
    }
}