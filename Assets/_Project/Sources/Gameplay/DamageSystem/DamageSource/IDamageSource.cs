namespace Asteroids.Gameplay.DamageSystem
{
    public interface IDamageSource
    {
        public float DamageCount { get; }
        public void Damage(IDamageable target);
    }
}