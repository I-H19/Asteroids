using System;
using _Project.Sources.Gameplay.DamageSystem.Damageable;
using UnityEngine;

namespace _Project.Sources.Gameplay.DamageSystem.DamageSource
{
    public class PlayerDamageSource : MonoBehaviour, IDamageSource
    {
        public float DamageCount { get; private set; }
        public event Action TargetDamaged;

        private HitDetectionType _hitDetectionType = HitDetectionType.HitSingle;

        private const int RAYCAST_BUFFER_SIZE = 128;
        private readonly RaycastHit2D[] _hits = new RaycastHit2D[RAYCAST_BUFFER_SIZE];
        private readonly ContactFilter2D  _contactFilter = ContactFilter2D.noFilter;

        public void Damage(IDamageable target)
        {
            target.TakeDamage(DamageCount);
            TargetDamaged?.Invoke();
        }

        public void ChangeDamage(float damage) => DamageCount = damage;
        public void ChangeHitType(HitDetectionType hitDetectionType) => _hitDetectionType = hitDetectionType;
        public void Tick()
        {
            Vector2 origin = transform.position;
            Vector2 laserDirection = transform.up;
            Vector2 bulletDirection = transform.forward;

            if (_hitDetectionType == HitDetectionType.HitAll)
            {
                int hitCount = Physics2D.Raycast(
                    origin,
                    laserDirection,
                    _contactFilter,
                    _hits);

                for (int i = 0; i < hitCount; i++)
                    TryDamage(_hits[i]);
            }
            else
                TryDamage(Physics2D.Raycast(origin, bulletDirection));

        }
        

        private void TryDamage(RaycastHit2D hit)
        {
            if (!hit) return;

            if (hit.transform.TryGetComponent(out EnemyLife enemyLife))
                Damage(enemyLife);
        }
    }
}