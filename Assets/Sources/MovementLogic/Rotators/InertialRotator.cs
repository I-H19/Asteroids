using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

public class InertialRotator : MonoBehaviour, IRotator
{
    public RotationDirection RotationDirection { get; private set; } = RotationDirection.None;

    private Rigidbody2D _rigidBody;
    private float _maxAngularSpeed;
    private float _angularAcceleration;
    private float _angularDeceleration;
    private float _stopEpsilon;

    public float CurrentAngularSpeed { get; private set; }

    private bool _rotateLeft;
    private bool _rotateRight;

    [Inject]
    public void Construct(Rigidbody2D rigidBody, InertialRotationSettings rotationSettings)
    {
        _rigidBody = rigidBody;

        _maxAngularSpeed = rotationSettings.MaxAngularSpeed;
        _angularAcceleration = rotationSettings.AngularAcceleration;
        _angularDeceleration = rotationSettings.AngularDeceleration;
        _stopEpsilon = rotationSettings.AngularStopEpsilon;
    }
    public void SetRotateLeft(bool value)
    {
        _rotateLeft = value;
        UpdateDirection();
    }
    public void SetRotateRight(bool value)
    {
        _rotateRight = value;
        UpdateDirection();
    }
    public void Rotate()
    {
        float deltaTime = Time.fixedDeltaTime;
        float angularVelocity = _rigidBody.angularVelocity;

        switch (RotationDirection)
        {
            case RotationDirection.Left:
                angularVelocity = Mathf.MoveTowards(
                    angularVelocity,
                    _maxAngularSpeed,
                    _angularAcceleration * deltaTime);
                break;

            case RotationDirection.Right:
                angularVelocity = Mathf.MoveTowards(
                    angularVelocity,
                    -_maxAngularSpeed,
                    _angularAcceleration * deltaTime);
                break;

            case RotationDirection.None:
                angularVelocity = Mathf.MoveTowards(
                    angularVelocity,
                    0f,
                    _angularDeceleration * deltaTime);
                break;
        }

        if (Mathf.Abs(angularVelocity) < _stopEpsilon)
            angularVelocity = 0f;

        _rigidBody.angularVelocity = angularVelocity;
        CurrentAngularSpeed = angularVelocity;
    }

    private void UpdateDirection()
    {
        if (_rotateLeft && !_rotateRight)
            RotationDirection = RotationDirection.Left;
        else if (_rotateRight && !_rotateLeft)
            RotationDirection = RotationDirection.Right;
        else
            RotationDirection = RotationDirection.None;
    }
}