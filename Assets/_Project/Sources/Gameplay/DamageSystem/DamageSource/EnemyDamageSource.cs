using _Project.Sources.Gameplay.DamageSystem.Damageable;
using UnityEngine;

namespace _Project.Sources.Gameplay.DamageSystem.DamageSource
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class EnemyDamageSource : MonoBehaviour, IDamageSource
    {
        public float DamageCount { get; private set; }

        public void Init(float damageCount) => DamageCount = damageCount;
        public void Damage(IDamageable target) => target.TakeDamage(DamageCount);

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent<PlayerLife>(out PlayerLife playerLife)) Damage(playerLife);
        }
    }
}