using UnityEngine;
using VContainer;

public class DirectionalRotator : MonoBehaviour, IRotator
{
    public float CurrentAngle { get; private set; }

    private Rigidbody2D _rigidBody;
    private float _rotationSpeed; 
    private bool _rotateLeft;
    private bool _rotateRight;

    [Inject]
    public void Construct(Rigidbody2D rigidBody, DirectionalRotationSettings rotationSettings)
    {
        _rigidBody = rigidBody;
        CurrentAngle = _rigidBody.rotation;
        
        _rotationSpeed = rotationSettings.RotationSpeed;
    }
    public void SetRotateLeft(bool value) => _rotateLeft = value;
    public void SetRotateRight(bool value) => _rotateRight = value;

    public void Rotate()
    {
        float deltaTime = Time.fixedDeltaTime;

        float direction = 0f;

        if (_rotateLeft && !_rotateRight)
            direction = 1f;
        else if (_rotateRight && !_rotateLeft)
            direction = -1f;

        _rigidBody.MoveRotation(
            _rigidBody.rotation + direction * _rotationSpeed * deltaTime
        );

        CurrentAngle = _rigidBody.rotation;
    }
}