using UnityEngine;

namespace Asteroids
{
    public class DirectionalMover : MonoBehaviour, IMover
    {
        private bool _enabled = true;
        private float _moveSpeed;
        private Rigidbody2D _rigidBody;


        public void EnableMoving()
        {
            _enabled = true;
        }

        public void DisableMoving()
        {
            _enabled = false;
            _rigidBody.linearVelocity = Vector2.zero;
        }

        public void Init(DirectionalMoverSettings moverSettings, Rigidbody2D moveRigidbody)
        {
            _rigidBody = moveRigidbody;
            _moveSpeed = moverSettings.MovingSpeed;
        }

        public void Move()
        {
            if (!_enabled)
            {
                return;
            }

            Vector2 forward = _rigidBody.transform.up;
            _rigidBody.linearVelocity = forward * _moveSpeed;
        }

        public void SetPosition(Vector2 position)
        {
            _rigidBody.position = position;
        }
    }
}