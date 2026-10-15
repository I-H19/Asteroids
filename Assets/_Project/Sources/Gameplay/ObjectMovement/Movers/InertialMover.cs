using System;
using _Project.Sources.Config.Movement;
using UnityEngine;

namespace _Project.Sources.Gameplay.ObjectMovement.Movers
{
    public class InertialMover : MonoBehaviour, IMover
    {
        public float CurrentSpeed { get; private set; }
        public event Action SpeedChanged;
        public event Action PositionChanged;

        private MovingDirection _movingDirection = MovingDirection.None;
        private Transform _objectTransform;
        private bool _isAcceleration;
        private bool _isBraking;
        private Rigidbody2D _rigidBody;
        private float _maxForwardSpeed;
        private float _forwardAcceleration;
        private float _coastDeceleration;
        private float _brakeDeceleration;
        private float _stopEpsilon;
        private float _oldSpeed;
        private bool _enabled = true;

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
            if (!_enabled) return;
            if (CurrentSpeed > 0)
                PositionChanged?.Invoke();


            Vector2 forward = _objectTransform.up;

            CurrentSpeed = Vector2.Dot(_rigidBody.linearVelocity, forward);
            if (!Mathf.Approximately(_oldSpeed, CurrentSpeed))
                SpeedChanged?.Invoke();

            float currentSpeed = Vector2.Dot(_rigidBody.linearVelocity, forward);
            float deltaTime = Time.fixedDeltaTime;

            switch (_movingDirection)
            {
                case MovingDirection.Acceleration:
                    currentSpeed = Mathf.MoveTowards(
                        currentSpeed,
                        _maxForwardSpeed,
                        _forwardAcceleration * deltaTime);
                    break;

                case MovingDirection.Bracking:
                    currentSpeed = Mathf.MoveTowards(
                        currentSpeed,
                        0f,
                        _brakeDeceleration * deltaTime);
                    break;

                case MovingDirection.None:
                    currentSpeed = Mathf.MoveTowards(
                        currentSpeed,
                        0f,
                        _coastDeceleration * deltaTime);
                    break;
            }

            if (currentSpeed < _stopEpsilon)
                currentSpeed = 0f;

            _rigidBody.linearVelocity = forward * currentSpeed;
        }

        public void StartAcceleration(bool value)
        {
            _isAcceleration = value;
            UpdateMovingDirection();
        }

        public void StartBraking(bool value)
        {
            _isBraking = value;
            UpdateMovingDirection();
        }

        public void EnableMoving()
            => _enabled = true;

        public void DisableMoving()
        {
            _enabled = false;
            if (!_enabled)
            {
                _isAcceleration = false;
                _isBraking = false;
                _movingDirection = MovingDirection.None;

                _rigidBody.linearVelocity = Vector2.zero;
            }
        }

        private void UpdateMovingDirection()
        {
            if (_isAcceleration && !_isBraking)
            {
                _movingDirection = MovingDirection.Acceleration;
            }
            else if (_isBraking && !_isAcceleration)
            {
                _movingDirection = MovingDirection.Bracking;
            }
            else
            {
                _movingDirection = MovingDirection.None;
            }
        }
    }
}