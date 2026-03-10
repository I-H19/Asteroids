using System;
using _Project.Sources.Settings.Movement;
using UnityEngine;

namespace _Project.Sources.Gameplay.ObjectMovement.Movers
{
    public class DirectionalMover : MonoBehaviour, IMover
    {
        public float CurrentSpeed { get; private set; }
        public MovingDirection MovingDirection { get; private set; } = MovingDirection.None;
        public Transform ObjectTransform { get; private set; }

        private bool _isMoving;
        private bool _isBraking;

        private Rigidbody2D _rigidBody;
        private float _moveSpeed;
        private bool _enabled = true;

        public void Init(IMoverSettings moverSettings, Rigidbody2D rigidbody)
        {
            if (moverSettings is not DirectionalMoverSettings)
                throw new Exception("Incorrect mover settings");

            DirectionalMoverSettings directionalMoverSettings = (DirectionalMoverSettings)moverSettings;

            _rigidBody = rigidbody;
            _moveSpeed = directionalMoverSettings.MovingSpeed;

            ObjectTransform = rigidbody.transform;
            CurrentSpeed = 0f;
        }

        public void Move()
        {
            if (!_enabled) return;

            Vector2 forward = ObjectTransform.up;

            float targetSpeed = 0f;

            switch (MovingDirection)
            {
                case MovingDirection.Acceleration:
                    targetSpeed = _moveSpeed;
                    break;

                case MovingDirection.Bracking:
                    targetSpeed = 0f;
                    break;

                case MovingDirection.None:
                    targetSpeed = 0f;
                    break;
            }

            CurrentSpeed = targetSpeed;
            _rigidBody.linearVelocity = forward * targetSpeed;
        }

        public void SetMoving(bool value)
        {
            _isMoving = value;
            UpdateMovingDirection();
        }

        public void SetBraking(bool value)
        {
            _isBraking = value;
            UpdateMovingDirection();
        }
        public void SetEnabled(bool enabled)
        {
            _enabled = enabled;
            if (!_enabled)
            {
                _isMoving = false;
                _isBraking = false;
                MovingDirection = MovingDirection.None;

                _rigidBody.linearVelocity = Vector2.zero;
            }
        }
        public void SetPosition(Vector2 position) => _rigidBody.position = position;

        private void UpdateMovingDirection()
        {
            if (_isMoving && !_isBraking)
            {
                MovingDirection = MovingDirection.Acceleration;
            }
            else if (_isBraking && !_isMoving)
            {
                MovingDirection = MovingDirection.Bracking;
            }
            else
            {
                MovingDirection = MovingDirection.None;
            }
        }
    }
}