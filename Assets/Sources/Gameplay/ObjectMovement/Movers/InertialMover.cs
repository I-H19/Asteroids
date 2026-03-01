using UnityEngine;
using VContainer;

public class InertialMover : MonoBehaviour, IMover
{
    public float CurrentSpeed { get; private set; }
    public MovingDirection MovingDirection { get; private set; } = MovingDirection.None;
    public Transform ObjectTransform { get; private set; }

    private bool _isAcceleration = false;
    private bool _isBracking = false;
    private Rigidbody2D _rigidBody;
    private float _maxForwardSpeed;
    private float _forwardAcceleration;
    private float _coastDeceleration;
    private float _brakeDeceleration;
    private float _stopEpsilon;

    public void Init(Rigidbody2D rigidbody, InertialMoverSettings moverSettings)
    {
        _rigidBody = rigidbody;
        _maxForwardSpeed = moverSettings.MaxForwardSpeed;
        _forwardAcceleration = moverSettings.ForwardAcceleration;
        _coastDeceleration = moverSettings.CoastDeceleration;
        _brakeDeceleration = moverSettings.BrakeDeceleration;
        _stopEpsilon = moverSettings.StopEpsilon;

        ObjectTransform = rigidbody.transform;
        CurrentSpeed = 0;
    }
    public void Move()
    {
        Vector2 forward = ObjectTransform.up;

        CurrentSpeed = Vector2.Dot(_rigidBody.linearVelocity, forward);

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
        _isBracking = value;
        UpdateMovingDirection();
    }
    private void UpdateMovingDirection()
    {
        if (_isAcceleration && !_isBracking)
        {
            MovingDirection = MovingDirection.Acceleration;
        }
        else if (_isBracking && !_isAcceleration)
        {
            MovingDirection = MovingDirection.Bracking;
        }
        else
        {
            MovingDirection = MovingDirection.None;
        }
    }

}

