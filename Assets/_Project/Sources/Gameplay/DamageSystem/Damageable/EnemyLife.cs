using System;
using UnityEngine;

namespace _Project.Sources.Gameplay.DamageSystem.Damageable
{
    public class EnemyLife : MonoBehaviour, IDamageable
    {
        public float Health { get; private set; }
        public Action OnDeath;
        public void TakeDamage(float damage)
        {
            Health -= damage;
            if (Health <= 0) EnemyKill();
        }

        public void EnemyKill()
        {
            OnDeath?.Invoke();
            Destroy(gameObject);
        }
    }
}
