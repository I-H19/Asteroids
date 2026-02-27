using UnityEngine;
using VContainer;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
public sealed class ScreenTeleporter : MonoBehaviour
{
    private Camera _camera;
    private Rigidbody2D _rigidbody;

    private float _leftScreenBorder;
    private float _rightScreenBorder;
    private float _bottomScreenBorder;
    private float _topScreenBorder;

    [Inject]
    public void Construct(Camera mainCamera)
    {
        _camera = mainCamera;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        Vector3 cameraWorldPosition = _camera.transform.position;

        float halfScreenHeightInWorldUnits = _camera.orthographicSize;
        float halfScreenWidthInWorldUnits = halfScreenHeightInWorldUnits * _camera.aspect;

        _leftScreenBorder = cameraWorldPosition.x - halfScreenWidthInWorldUnits;
        _rightScreenBorder = cameraWorldPosition.x + halfScreenWidthInWorldUnits;
        _bottomScreenBorder = cameraWorldPosition.y - halfScreenHeightInWorldUnits;
        _topScreenBorder = cameraWorldPosition.y + halfScreenHeightInWorldUnits;
    }

    private void FixedUpdate()
    {
        Vector2 currentRigidbodyPosition = _rigidbody.position;

        if (currentRigidbodyPosition.x > _rightScreenBorder)
            currentRigidbodyPosition.x = _leftScreenBorder;
        else if (currentRigidbodyPosition.x < _leftScreenBorder)
            currentRigidbodyPosition.x = _rightScreenBorder;

        if (currentRigidbodyPosition.y > _topScreenBorder)
            currentRigidbodyPosition.y = _bottomScreenBorder;
        else if (currentRigidbodyPosition.y < _bottomScreenBorder)
            currentRigidbodyPosition.y = _topScreenBorder;

        _rigidbody.position = currentRigidbodyPosition;
    }
}