using System;
using UnityEngine;

public class PlayerLife : MonoBehaviour, IDamageable
{
    public float Health { get; private set; }
    public Action OnDeath;
    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0) PlayerKill();
    }

    public void PlayerKill()
    {
        OnDeath?.Invoke();
        Destroy(gameObject);
    }
}
