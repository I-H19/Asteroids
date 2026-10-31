using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Asteroids
{
    public class BulletSpawner : IPausable, ISceneTickable, ISceneFixedTickable
    {
        private readonly List<Bullet> _bullets = new();
        private float _bulletCooldown;
        private float _bulletDamage;
        private DirectionalMoverSettings _bulletMoverSettings;
        private Bullet _bulletPrefab;
        private bool _isPause;

        private bool _isSpawning;
        private float _nextShotTime;
        private Player _player;
        private IObjectResolver _resolver;

        public void Pause()
        {
            _isPause = true;
            StopSpawning();
            foreach (Bullet bullet in _bullets)
            {
                bullet.Pause();
            }
        }

        public void Unpause()
        {
            _isPause = false;
            foreach (Bullet bullet in _bullets)
            {
                bullet.Unpause();
            }
        }

        public void Tick()
        {
            if (_isPause)
            {
                return;
            }

            if (!_isSpawning)
            {
                return;
            }

            if (!(Time.time >= _nextShotTime))
            {
                return;
            }

            SpawnBullet();
            _nextShotTime = Time.time + _bulletCooldown;
        }

        public void FixedTick()
        {
            if (_isPause)
            {
                return;
            }

            foreach (Bullet bullet in _bullets)
            {
                bullet.FixedTick();
            }
        }

        [Inject]
        public void Construct(
            IObjectResolver resolver,
            PrefabHolder spawnerPrefabs,
            PlayerCombatSettings playerCombatSettings)
        {
            _resolver = resolver;
            _bulletPrefab = spawnerPrefabs.BulletPrefab;
            _bulletCooldown = playerCombatSettings.BulletCooldown;
            _bulletMoverSettings = playerCombatSettings.BulletMoverSettings;
            _bulletDamage = playerCombatSettings.BulletDamage;

            _nextShotTime = Time.time;
        }

        public void Init(Player player)
        {
            _player = player;
        }

        public void StartSpawning()
        {
            _isSpawning = true;
        }

        public void StopSpawning()
        {
            _isSpawning = false;
        }

        private void SpawnBullet()
        {
            Bullet bullet =
                _resolver.Instantiate(_bulletPrefab, _player.transform.position, _player.transform.rotation);

            bullet.Init(_bulletMoverSettings, _bulletDamage);
            _bullets.Add(bullet);

            bullet.Destroyed += OnBulletDestroyed;
        }

        private void OnBulletDestroyed(Bullet bullet)
        {
            _bullets.Remove(bullet);
            bullet.Destroyed -= OnBulletDestroyed;
        }
    }
}
