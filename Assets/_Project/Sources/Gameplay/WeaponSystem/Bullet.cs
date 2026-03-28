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
    public class Bullet : MonoBehaviour
    {
        public event Action<Bullet> Destroyed;

        private DirectionalMover _mover;
        private PlayerDamageSource _damageSource;
        private Camera _camera;
        private ScreenBoundsTracker _screenBoundsTracker;

        [Inject]
        public void Construct(Camera mainCamera) => _camera = mainCamera;

        public void Init(IMoverSettings bulletMoverSettings, float bulletDamage)
        {
            _mover = GetComponent<DirectionalMover>();
            _damageSource = GetComponent<PlayerDamageSource>();
            _damageSource.ChangeDamage(bulletDamage);
            Rigidbody2D rigidBodyTemplate = GetComponent<Rigidbody2D>();
            
            _mover.Init(bulletMoverSettings, rigidBodyTemplate);
            _damageSource.TargetDamaged += OnHit;
            
            _screenBoundsTracker = new ScreenBoundsTracker();
            _screenBoundsTracker.Init(rigidBodyTemplate, _camera);
            
            _mover.SetMoving(true);
        }

        public void OnHit() => Destroy(gameObject);
        public void Tick()
        {
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
    }
}