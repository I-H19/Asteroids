namespace _Project.Sources.Gameplay.DamageSystem.Damageable
{
    public interface IDamageable
    {
        public float Health { get; }
        public void TakeDamage(float damage);
    }
}