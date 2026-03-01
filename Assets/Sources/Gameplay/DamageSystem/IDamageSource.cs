public interface IDamageSource
{
    public float DamageCount { get; }
    public void Damage(IDamageable target);
}