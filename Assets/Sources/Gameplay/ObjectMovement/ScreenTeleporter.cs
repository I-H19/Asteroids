using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(ScreenBoundsTracker))]
public sealed class ScreenTeleporter : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private ScreenBoundsTracker _boundsTracker;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _boundsTracker = GetComponent<ScreenBoundsTracker>();
    }

    private void FixedUpdate()
    {
        if (!_boundsTracker.IsOutOfBounds())
            return;

        _rigidbody.position = _boundsTracker.GetTeleportPosition();
    }
}