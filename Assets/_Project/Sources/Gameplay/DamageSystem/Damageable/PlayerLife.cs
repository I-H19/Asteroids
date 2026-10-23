using System;
using UnityEngine;

namespace Asteroids
{
    public class PlayerLife : MonoBehaviour, IDamageable
    {
        private float _health;

        public void TakeDamage(float damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                PlayerKill();
            }
        }

        public event Action Died;

        public void Init(float healthCount)
        {
            _health = healthCount;
        }

        private void PlayerKill()
        {
            Died?.Invoke();
        }
    }
}