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
        private bool _isPause;

        private DirectionalMover _mover;
        private ScreenBoundsTracker _screenBoundsTracker;

        private void OnDestroy()
        {
            Destroyed?.Invoke(this);
            _damageSource.TargetDamaged -= OnHit;
        }

        public void Pause()
        {
            _mover.DisableMoving();
            _isPause = true;
        }

        public void Unpause()
        {
            _mover.EnableMoving();
            _isPause = false;
        }

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

        private void OnHit()
        {
            Destroy(gameObject);
        }

        public void FixedTick()
        {
            if (_isPause)
            {
                return;
            }

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
    }
}
