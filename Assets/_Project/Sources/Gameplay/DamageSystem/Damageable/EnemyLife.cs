using System;
using UnityEngine;

namespace Asteroids
{
    public class EnemyLife : MonoBehaviour, IDamageable
    {
        private float _health;

        public event Action Died;

        public void TakeDamage(float damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                EnemyKill();
            }
        }

        private void EnemyKill()
        {
            Died?.Invoke();
        }
    }
}