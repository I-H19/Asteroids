using System;
using UnityEngine;

[RequireComponent(typeof(ScreenBoundsTracker))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerDamageSource))]
[RequireComponent(typeof(DirectionalMover))]
public class Bullet : MonoBehaviour
{

    [SerializeField] private DirectionalMoverSettings _moverSettings = new();
    private DirectionalMover _mover;
    private PlayerDamageSource _damageSource;
    private ScreenBoundsTracker _boundsTracker;

    public void Init(float bulletSpeed, float bulletDamage)
    {
        _boundsTracker = GetComponent<ScreenBoundsTracker>();

        _mover = GetComponent<DirectionalMover>();
        _damageSource = GetComponent<PlayerDamageSource>();
        _damageSource.ChangeDamage(bulletDamage);

        _moverSettings.ChangeValues(bulletSpeed);

        _mover.Init(_moverSettings, GetComponent<Rigidbody2D>());
        _damageSource.TargetDamaged += OnHit;

        _mover.SetMoving(true);

    }

    public void OnHit() => Destroy(gameObject);

    internal void Init(float bulletSpeed, object bulletDamage)
    {
        throw new NotImplementedException();
    }

    private void FixedUpdate()
    {
        if (_boundsTracker.IsOutOfBounds())
            OnHit();
        else
            _mover.Move();
    }

    private void OnDestroy() => _damageSource.TargetDamaged -= OnHit;
}