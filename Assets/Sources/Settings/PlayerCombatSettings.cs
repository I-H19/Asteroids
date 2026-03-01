using UnityEngine;

public class PlayerCombatSettings : MonoBehaviour
{
    [field: SerializeField] public float BulletCooldown { get; private set; } = 5f;
    [field: SerializeField] public float BulletSpeed { get; private set; } = 1f;
    [field: SerializeField] public float BulletDamage { get; private set; } = 5f;
    [field: SerializeField] public float LaserDamage { get; private set; } = 10f;
    [field: SerializeField] public float LaserOperatingTime { get; private set; } = 5f;
    [field: SerializeField] public float LaserChargeReloadingTime { get; private set; } = 5f;
    [field: SerializeField] public float LaserShootCooldown { get; private set; } = 5f;
    [field: SerializeField] public int MaxLaserCharge { get; private set; } = 5;
}