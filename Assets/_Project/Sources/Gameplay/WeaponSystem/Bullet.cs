using System;
using UnityEngine;
using VContainer;

namespace Asteroids
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PlayerDamageSource))]
    [RequireComponent(typeof(DirectionalMover))]
    public class Bullet : MonoBehaviour, IPausable
    {
        private PlayerDamageSource _damageSource;
        private bool _isDestroying;

        private DirectionalMover _mover;
        private ScreenBoundsTracker _screenBoundsTracker;

        public event Action<Bullet> Destroyed;

        [Inject]
        public void Construct(ScreenBoundsTracker screenBoundsTracker)
        {
            _screenBoundsTracker = screenBoundsTracker;
        }

        public void Init(DirectionalMoverSettings bulletMoverSettings, float bulletDamage)
        {
            _mover = GetComponent<DirectionalMover>();
            _damageSource = GetComponent<PlayerDamageSource>();
            _damageSource.ChangeDamage(bulletDamage);
            Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();

            _mover.Init(bulletMoverSettings, rigidBody);
            _damageSource.TargetDamaged += OnHit;

            _screenBoundsTracker.Init(rigidBody);

            _mover.EnableMoving();
        }

        private void OnDestroy()
        {
            Destroyed?.Invoke(this);
            if (_damageSource != null)
            {
                _damageSource.TargetDamaged -= OnHit;
            }
        }

        public void Pause()
        {
            _mover.DisableMoving();
        }

        public void Unpause()
        {
            _mover.EnableMoving();
        }

        public void FixedTick()
        {
            _damageSource.Tick();

            if (_screenBoundsTracker.IsOutOfBounds())
            {
                OnHit();
            }
            else
            {
                _mover.Move();
            }
        }

        public void DestroyBullet()
        {
            if (_isDestroying)
            {
                return;
            }

            _isDestroying = true;
            Destroy(gameObject);
        }

        private void OnHit()
        {
            DestroyBullet();
        }
    }
}