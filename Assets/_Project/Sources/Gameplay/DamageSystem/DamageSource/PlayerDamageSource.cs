using System;
using _Project.Sources.Gameplay.DamageSystem.Damageable;
using UnityEngine;

namespace _Project.Sources.Gameplay.DamageSystem.DamageSource
{
    public class PlayerDamageSource : MonoBehaviour, IDamageSource
    {
        public float DamageCount { get; private set; }
        public event Action TargetDamaged;

        private bool _isLaser;

        public void Damage(IDamageable target)
        {
            target.TakeDamage(DamageCount);
            TargetDamaged?.Invoke();
        }

        public void ChangeDamage(float damage) => DamageCount = damage;
        public void ChangeType(bool isLaser) => _isLaser = isLaser;
        public void Tick()
        {
            Vector2 origin = transform.position;
            Vector2 laserDirection = transform.up;
            Vector2 bulletDirection = transform.forward;

            if (_isLaser)
            {
                RaycastHit2D[] laserHits = Physics2D.RaycastAll(origin, laserDirection);
                foreach (RaycastHit2D hit in laserHits)
                    TryDamage(hit);
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