using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class BulletSpawner
{
    private Player _player;
    private GameObject _bulletTemplate;
    private float _bulletCooldown;
    private float _bulletSpeed;
    private IObjectResolver _resolver;

    private bool _isSpawning;
    private float _nextShotTime;
    private float _bulletDamage;

    private List<Bullet> _bullets = new();

    [Inject]
    public void Construct(IObjectResolver resolver, PrefabHolder spawnerPrefabs, PlayerCombatSettings playerCombatSettings, Player player)
    {
        _player = player;
        _resolver = resolver;
        _bulletTemplate = spawnerPrefabs.Bullet;
        _bulletCooldown = playerCombatSettings.BulletCooldown;
        _bulletSpeed = playerCombatSettings.BulletSpeed;
        _bulletDamage = playerCombatSettings.BulletDamage;

        _nextShotTime = Time.time; 
    }

    public void StartSpawning() => _isSpawning = true;
    public void StopSpawning() => _isSpawning = false;

    public void Tick()
    {
        foreach (Bullet bullet in _bullets) bullet.Tick();

        if (!_isSpawning) return;

        if (Time.time >= _nextShotTime)
        {
            SpawnBullet();
            _nextShotTime = Time.time + _bulletCooldown;
        }
    }
    private void SpawnBullet()
    {
        GameObject bulletTemplate = _resolver.Instantiate(_bulletTemplate, _player.transform.position, _player.transform.rotation);

        if (bulletTemplate.TryGetComponent<Bullet>(out Bullet bullet))
        {
            bullet.Init(_bulletSpeed, _bulletDamage);
            _bullets.Add(bullet);

            bullet.Destroyed += OnBulletDestroy;
        }
    }

    private void OnBulletDestroy(Bullet bullet)
    {
        _bullets.Remove(bullet);
        bullet.Destroyed -= OnBulletDestroy;
    }
}