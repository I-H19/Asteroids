using System;
using UnityEngine;

namespace Asteroids
{
    public class EnemyLife : MonoBehaviour, IDamageable
    {
        private float _health;
        
        public void TakeDamage(float damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                EnemyKill();
            }
        }

        public event Action Died;

        private void EnemyKill()
        {
            Died?.Invoke();
            Destroy(gameObject);
        }
    }
}