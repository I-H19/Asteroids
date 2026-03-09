using Asteroids.Gameplay.DamageSystem;
using Asteroids.Gameplay.ObjectMovement;
using Asteroids.Settings;
using System;
using UnityEngine;

namespace Asteroids.Gameplay.WeaponSystem
{
    [RequireComponent(typeof(ScreenBoundsTracker))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PlayerDamageSource))]
    [RequireComponent(typeof(DirectionalMover))]
    public class Bullet : MonoBehaviour
    {
        public Action<Bullet> Destroyed;

        private readonly DirectionalMoverSettings _moverSettings = new();
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
        public void Tick()
        {
            _damageSource.Tick();

            if (_boundsTracker.IsOutOfBounds())
                OnHit();
            else
                _mover.Move();
        }
        private void OnDestroy()
        {
            Destroyed?.Invoke(this);
            _damageSource.TargetDamaged -= OnHit;
        }
    }
}