using _Project.Sources.Config.Movement;
using UnityEngine;

namespace _Project.Sources.Gameplay.ObjectMovement.Movers
{
    public class DirectionalMover : MonoBehaviour, IMover
    {
        private Rigidbody2D _rigidBody;
        private float _moveSpeed;
        private bool _enabled = true;

        public void Init(DirectionalMoverSettings moverSettings, Rigidbody2D moveRigidbody)
        {
            _rigidBody = moveRigidbody;
            _moveSpeed = moverSettings.MovingSpeed;
        }

        public void Move()
        {
            if (!_enabled) return;

            Vector2 forward = _rigidBody.transform.up;
            _rigidBody.linearVelocity = forward * _moveSpeed;
        }
        

        public void EnableMoving()
        {
            _enabled = true;   
        }

        public void DisableMoving()
        {
            _enabled = false;
            _rigidBody.linearVelocity = Vector2.zero;
        }
        public void SetPosition(Vector2 position) => _rigidBody.position = position;
    }
}