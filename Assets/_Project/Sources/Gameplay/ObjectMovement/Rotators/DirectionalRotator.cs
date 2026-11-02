using System;
using UnityEngine;

namespace Asteroids
{
    public class DirectionalRotator : MonoBehaviour
    {
        private bool _enabled = true;

        private Rigidbody2D _rigidBody;
        private bool _rotateLeft;
        private bool _rotateRight;

        private float _rotationSpeed;
        public float CurrentAngle { get; private set; }

        public event Action AngleChanged;

        public void Init(Rigidbody2D rigidBody, DirectionalRotationSettings rotationSettings)
        {
            _rigidBody = rigidBody;
            CurrentAngle = _rigidBody.rotation;

            _rotationSpeed = rotationSettings.RotationSpeed;
        }

        public void Rotate()
        {
            if (!_enabled)
            {
                return;
            }

            float deltaTime = Time.fixedDeltaTime;

            float direction = 0f;

            if (_rotateLeft && !_rotateRight)
            {
                direction = 1f;
            }
            else if (_rotateRight && !_rotateLeft)
            {
                direction = -1f;
            }

            _rigidBody.MoveRotation(_rigidBody.rotation + direction * _rotationSpeed * deltaTime);

            CurrentAngle = transform.eulerAngles.z;
            AngleChanged?.Invoke();
        }

        public void SetEnabled(bool enabledState)
        {
            _enabled = enabledState;
            if (_enabled)
            {
                return;
            }

            _rigidBody.angularVelocity = 0f;
            _rotateLeft = false;
            _rotateRight = false;
        }

        public void SetRotateLeft(bool value)
        {
            _rotateLeft = value;
        }

        public void SetRotateRight(bool value)
        {
            _rotateRight = value;
        }
    }
}