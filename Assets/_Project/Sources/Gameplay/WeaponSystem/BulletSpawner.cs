using System.Collections.Generic;
using _Project.Sources.Config;
using _Project.Sources.Config.Movement;
using _Project.Sources.GameLoop;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Sources.Gameplay.WeaponSystem
{
    public class BulletSpawner : IPausable, ISceneTickable
    {
        private Player _player;
        private Bullet _bulletPrefab;
        private float _bulletCooldown;
        private DirectionalMoverSettings _bulletMoverSettings;
        private IObjectResolver _resolver;

        private bool _isSpawning;
        private bool _isPause;
        private float _nextShotTime;
        private float _bulletDamage;

        private readonly List<Bullet> _bullets = new();
        
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

        public void Init(Player player) => _player = player;

        public void StartSpawning() => _isSpawning = true;
        public void StopSpawning() => _isSpawning = false;

        public void Tick()
        {
            if(_isPause) return;
            
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
            Bullet bullet =
                _resolver.Instantiate(_bulletPrefab, _player.transform.position, _player.transform.rotation);

            bullet.Init(_bulletMoverSettings, _bulletDamage);
            _bullets.Add(bullet);

            bullet.Destroyed += OnBulletDestroy;
        }

        private void OnBulletDestroy(Bullet bullet)
        {
            _bullets.Remove(bullet);
            bullet.Destroyed -= OnBulletDestroy;
        }

        public void Pause()
        {
            _isPause = true;
            StopSpawning();
            foreach (Bullet bullet in _bullets) bullet.Pause();
        }

        public void Unpause()
        {
            _isPause = false;
            foreach (Bullet bullet in _bullets) bullet.Unpause();
        }
    }
}
