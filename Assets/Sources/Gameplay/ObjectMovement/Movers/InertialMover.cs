using System;
using UnityEngine;

public class InertialMover : MonoBehaviour, IMover
{
    public float CurrentSpeed { get; private set; }
    public Action SpeedChanged;
    public MovingDirection MovingDirection { get; private set; } = MovingDirection.None;
    public Transform ObjectTransform { get; private set; }

    private bool _isAcceleration = false;
    private bool _isBraking = false;
    private Rigidbody2D _rigidBody;
    private float _maxForwardSpeed;
    private float _forwardAcceleration;
    private float _coastDeceleration;
    private float _brakeDeceleration;
    private float _stopEpsilon;
    private bool _enabled = true;

    public void Init(IMoverSettings moverSettings, Rigidbody2D rigidbody)
    {
        if (moverSettings is not InertialMoverSettings)
            throw new Exception("Incorrect mover settings");

        InertialMoverSettings inertialMoverSettings = (InertialMoverSettings)moverSettings;

        _rigidBody = rigidbody;
        _maxForwardSpeed = inertialMoverSettings.MaxForwardSpeed;
        _forwardAcceleration = inertialMoverSettings.ForwardAcceleration;
        _coastDeceleration = inertialMoverSettings.CoastDeceleration;
        _brakeDeceleration = inertialMoverSettings.BrakeDeceleration;
        _stopEpsilon = inertialMoverSettings.StopEpsilon;

        ObjectTransform = rigidbody.transform;
        CurrentSpeed = 0;
    }
    public void Move()
    {
        if (!_enabled) return;

        Vector2 forward = ObjectTransform.up;

        CurrentSpeed = Vector2.Dot(_rigidBody.linearVelocity, forward);
        SpeedChanged?.Invoke();

        float currentSpeed = Vector2.Dot(_rigidBody.linearVelocity, forward);
        float deltaTime = Time.fixedDeltaTime;

        switch (MovingDirection)
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
    public void SetMoving(bool value)
    {
        _isAcceleration = value;
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
            _isAcceleration = false;
            _isBraking = false;
            MovingDirection = MovingDirection.None;

            _rigidBody.linearVelocity = Vector2.zero;
        }
    }
    private void UpdateMovingDirection()
    {
        if (_isAcceleration && !_isBraking)
        {
            MovingDirection = MovingDirection.Acceleration;
        }
        else if (_isBraking && !_isAcceleration)
        {
            MovingDirection = MovingDirection.Bracking;
        }
        else
        {
            MovingDirection = MovingDirection.None;
        }
    }

}

