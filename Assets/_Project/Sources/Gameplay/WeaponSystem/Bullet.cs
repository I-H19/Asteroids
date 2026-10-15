using System;
using _Project.Sources.Config.Movement;
using _Project.Sources.Gameplay.DamageSystem.DamageSource;
using _Project.Sources.Gameplay.ObjectMovement;
using _Project.Sources.Gameplay.ObjectMovement.Movers;
using UnityEngine;
using VContainer;

namespace _Project.Sources.Gameplay.WeaponSystem
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PlayerDamageSource))]
    [RequireComponent(typeof(DirectionalMover))]
    public class Bullet : MonoBehaviour, IPausable
    {
        public event Action<Bullet> Destroyed;

        private DirectionalMover _mover;
        private PlayerDamageSource _damageSource;
        private Camera _camera;
        private ScreenBoundsTracker _screenBoundsTracker;
        private bool _isPause;

        [Inject]
        public void Construct(Camera mainCamera, ScreenBoundsTracker screenBoundsTracker)
        {
            _camera = mainCamera;
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
            
            _screenBoundsTracker.Init(rigidBody, _camera);
            
            _mover.EnableMoving();
        }

        public void OnHit() => Destroy(gameObject);
        public void Tick()
        {
            if (_isPause) return;
            
            _damageSource.Tick();

            if (_screenBoundsTracker.IsOutOfBounds())
                OnHit();
            else
                _mover.Move();
        }
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
    }
}