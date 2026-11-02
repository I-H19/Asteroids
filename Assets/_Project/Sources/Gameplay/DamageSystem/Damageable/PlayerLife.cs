using System;
using UnityEngine;

namespace Asteroids
{
    public class PlayerLife : MonoBehaviour, IDamageable
    {
        private float _health;
        private float _maxHealth;

        public event Action Died;

        public void Init(float maxHealthCount)
        {
            _health = maxHealthCount;
            _maxHealth = maxHealthCount;
        }

        public void TakeDamage(float damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                PlayerKill();
            }
        }

        public void RestoreHealth()
        {
            _health = _maxHealth;
        }

        private void PlayerKill()
        {
            Died?.Invoke();
        }
    }
}