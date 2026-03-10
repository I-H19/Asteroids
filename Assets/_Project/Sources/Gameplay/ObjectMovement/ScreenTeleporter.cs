using _Project.Sources.GameLoop;
using UnityEngine;

namespace _Project.Sources.Gameplay.ObjectMovement
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(ScreenBoundsTracker))]
    public sealed class ScreenTeleporter : MonoBehaviour, ISceneTickable
    {
        private Rigidbody2D _rigidbody;
        private ScreenBoundsTracker _boundsTracker;

        public void Init()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _boundsTracker = GetComponent<ScreenBoundsTracker>();
        }
        public void Tick()
        {
            if (!_boundsTracker.IsOutOfBounds())
                return;

            _rigidbody.position = _boundsTracker.GetTeleportPosition();
        }
    }
}