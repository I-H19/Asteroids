using UnityEngine;

namespace Asteroids
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class EnemyDamageSource : MonoBehaviour
    {
        public float DamageCount { get; private set; }

        public void Init(float damageCount)
        {
            DamageCount = damageCount;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out PlayerLife playerLife))
            {
                Damage(playerLife);
            }
        }

        public void Damage(IDamageable target)
        {
            target.TakeDamage(DamageCount);
        }
    }
}