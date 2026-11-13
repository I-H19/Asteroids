using System;
using UnityEngine;

namespace Asteroids
{
    public class EnemyLife : MonoBehaviour, IDamageable
    {
        private float _health;
        private bool _isDead;
        
        public event Action Died;

        public void Init(float maxHealth)
        {
            _health = maxHealth;
            _isDead = false;
        }
        public void TakeDamage(float damage)
        {
            if (_isDead)
            {
                throw new InvalidOperationException("Cannot apply damage to an enemy (is already dead)");
            }

            if (_health <= 0)
            {
                throw new NullReferenceException("Health value is not initialized (health <= 0)");
            }

            _health -= damage;
            if (_health <= 0)
            {
                EnemyKill();
            }
        }

        private void EnemyKill()
        {
            _isDead = true;
            Died?.Invoke();
        }
    }
}