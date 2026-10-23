using System;
using UnityEngine;

namespace Asteroids
{
    public class InertialMover : MonoBehaviour, IMover
    {
        private float _brakeDeceleration;
        private float _coastDeceleration;
        private bool _enabled = true;
        private float _forwardAcceleration;
        private bool _isAccelerating;
        private bool _isBraking;
        private float _maxForwardSpeed;

        private MovingDirection _movingDirection = MovingDirection.None;
        private Transform _objectTransform;
        private float _oldSpeed;
        private Rigidbody2D _rigidBody;
        private float _stopEpsilon;
        public float CurrentSpeed { get; private set; }

        public void EnableMoving()
        {
            _enabled = true;
        }

        public void DisableMoving()
        {
            _enabled = false;
            
            _isAccelerating = false;
            _isBraking = false;
            _movingDirection = MovingDirection.None;

            _rigidBody.linearVelocity = Vector2.zero;
        }

        public event Action SpeedChanged;
        public event Action PositionChanged;

        public void Init(InertialMoverSettings moverSettings, Rigidbody2D moverRigidbody)
        {
            _rigidBody = moverRigidbody;
            _maxForwardSpeed = moverSettings.MaxForwardSpeed;
            _forwardAcceleration = moverSettings.ForwardAcceleration;
            _coastDeceleration = moverSettings.CoastDeceleration;
            _brakeDeceleration = moverSettings.BrakeDeceleration;
            _stopEpsilon = moverSettings.StopEpsilon;

            _objectTransform = moverRigidbody.transform;
            CurrentSpeed = 0;
        }

        public void Move()
        {
            _oldSpeed = CurrentSpeed;
            if (!_enabled)
            {
                return;
            }

            if (CurrentSpeed > 0)
            {
                PositionChanged?.Invoke();
            }


            Vector2 forward = _objectTransform.up;

            CurrentSpeed = Vector2.Dot(_rigidBody.linearVelocity, forward);
            if (!Mathf.Approximately(_oldSpeed, CurrentSpeed))
            {
                SpeedChanged?.Invoke();
            }

            float currentSpeed = Vector2.Dot(_rigidBody.linearVelocity, forward);
            float deltaTime = Time.fixedDeltaTime;

            currentSpeed = _movingDirection switch
            {
                MovingDirection.Acceleration => Mathf.MoveTowards(
                    currentSpeed,
                    _maxForwardSpeed,
                    _forwardAcceleration * deltaTime),
                MovingDirection.Braking => Mathf.MoveTowards(currentSpeed, 0f, _brakeDeceleration * deltaTime),
                MovingDirection.None => Mathf.MoveTowards(currentSpeed, 0f, _coastDeceleration * deltaTime),
                _ => currentSpeed
            };

            if (currentSpeed < _stopEpsilon)
            {
                currentSpeed = 0f;
            }

            _rigidBody.linearVelocity = forward * currentSpeed;
        }

        public void SetAccelerating(bool isAccelerating)
        {
            _isAccelerating = isAccelerating;
            UpdateMovingDirection();
        }

        public void SetBraking(bool isBraking)
        {
            _isBraking = isBraking;
            UpdateMovingDirection();
        }

        private void UpdateMovingDirection()
        {
            if (_isAccelerating && !_isBraking)
            {
                _movingDirection = MovingDirection.Acceleration;
            }
            else if (_isBraking && !_isAccelerating)
            {
                _movingDirection = MovingDirection.Braking;
            }
            else
            {
                _movingDirection = MovingDirection.None;
            }
        }
    }
}