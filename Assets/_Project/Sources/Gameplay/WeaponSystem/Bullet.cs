using System;
using _Project.Sources.Config.Movement;
using _Project.Sources.Gameplay.DamageSystem.DamageSource;
using _Project.Sources.Gameplay.ObjectMovement;
using _Project.Sources.Gameplay.ObjectMovement.Movers;
using UnityEngine;

namespace _Project.Sources.Gameplay.WeaponSystem
{
    [RequireComponent(typeof(ScreenBoundsTracker))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PlayerDamageSource))]
    [RequireComponent(typeof(DirectionalMover))]
    public class Bullet : MonoBehaviour
    {
        public Action<Bullet> Destroyed;

        private DirectionalMover _mover;
        private PlayerDamageSource _damageSource;
        private ScreenBoundsTracker _boundsTracker;

        public void Init(IMoverSettings bulletMoverSettings, float bulletDamage)
        {
            _boundsTracker = GetComponent<ScreenBoundsTracker>();

            _mover = GetComponent<DirectionalMover>();
            _damageSource = GetComponent<PlayerDamageSource>();
            _damageSource.ChangeDamage(bulletDamage);
            
            _mover.Init(bulletMoverSettings, GetComponent<Rigidbody2D>());
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