using UnityEngine;

namespace Asteroids
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class EnemyDamageSource : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out PlayerLife playerLife))
            {
                Damage(playerLife);
            }
        }

        public float DamageCount { get; private set; }

        public void Damage(IDamageable target)
        {
            target.TakeDamage(DamageCount);
        }

        public void Init(float damageCount)
        {
            DamageCount = damageCount;
        }
    }
}