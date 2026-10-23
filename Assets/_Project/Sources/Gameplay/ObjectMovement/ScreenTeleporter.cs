using UnityEngine;

namespace Asteroids
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class ScreenTeleporter : MonoBehaviour, ISceneTickable
    {
        private ScreenBoundsTracker _boundsTracker;
        private Rigidbody2D _rigidbody;

        public void Tick()
        {
            if (!_boundsTracker.IsOutOfBounds())
            {
                return;
            }

            _rigidbody.position = _boundsTracker.GetTeleportPosition();
        }

        public void Init(ScreenBoundsTracker tracker)
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _boundsTracker = tracker;
        }
    }
}