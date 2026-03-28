using _Project.Sources.GameLoop;
using UnityEngine;

namespace _Project.Sources.Gameplay.ObjectMovement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class ScreenTeleporter : MonoBehaviour, ISceneTickable
    {
        private Rigidbody2D _rigidbody;
        private ScreenBoundsTracker _boundsTracker;

        public void Init(ScreenBoundsTracker tracker) 
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _boundsTracker = tracker;
        }
        public void Tick()
        {
            if (!_boundsTracker.IsOutOfBounds())
                return;

            _rigidbody.position = _boundsTracker.GetTeleportPosition();
        }
    }
}