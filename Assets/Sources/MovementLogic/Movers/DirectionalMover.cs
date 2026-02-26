using UnityEngine;
using VContainer;

public class DirectionalMover : MonoBehaviour, IMover
{
    public float CurrentSpeed { get; private set; }
    public MovingDirection MovingDirection { get; private set; } = MovingDirection.None;
    public Transform ObjectTransform { get; private set; }

    private bool _isMoving;
    private bool _isBraking;

    private Rigidbody2D _rigidBody;
    private float _moveSpeed;

    [Inject]
    public void Construct(DirectionalMoverSettings moverSettings, Rigidbody2D rigidbody)
    {
        _rigidBody = rigidbody;
        _moveSpeed = moverSettings.MovingSpeed;

        ObjectTransform = rigidbody.transform;
        CurrentSpeed = 0f;
    }

    public void Move()
    {
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