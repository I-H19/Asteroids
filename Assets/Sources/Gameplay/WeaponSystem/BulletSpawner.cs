using UnityEngine;
using VContainer;
using VContainer.Unity;

public class BulletSpawner : MonoBehaviour
{
    private GameObject _bulletTemplate;
    private float _bulletCooldown;
    private float _bulletSpeed;
    private IObjectResolver _resolver;

    private bool _isSpawning;
    private float _nextShotTime;
    private float _bulletDamage;

    [Inject]
    public void Construct(IObjectResolver resolver, PrefabHolder spawnerPrefabs, PlayerCombatSettings playerCombatSettings)
    {
        _resolver = resolver;
        _bulletTemplate = spawnerPrefabs.Bullet;
        _bulletCooldown = playerCombatSettings.BulletCooldown;
        _bulletSpeed = playerCombatSettings.BulletSpeed;
        _bulletDamage = playerCombatSettings.BulletDamage;

        _nextShotTime = Time.time; 
    }

    public void StartSpawning() => _isSpawning = true;
    public void StopSpawning() => _isSpawning = false;

    private void Update()
    {
        if (!_isSpawning)
            return;

        if (Time.time >= _nextShotTime)
        {
            SpawnBullet();
            _nextShotTime = Time.time + _bulletCooldown;
        }
    }
    private void SpawnBullet()
    {
        GameObject bulletTemplate = _resolver.Instantiate(_bulletTemplate, transform.position, transform.rotation);

        if (bulletTemplate.TryGetComponent<Bullet>(out var bullet))
        {
            bullet.Init(_bulletSpeed, _bulletDamage);
        }
    }
}