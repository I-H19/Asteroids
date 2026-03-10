using _Project.Sources.Gameplay.DamageSystem.Damageable;

namespace _Project.Sources.Gameplay.DamageSystem.DamageSource
{
    public interface IDamageSource
    {
        public float DamageCount { get; }
        public void Damage(IDamageable target);
    }
}