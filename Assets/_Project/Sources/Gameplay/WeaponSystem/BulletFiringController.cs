using UnityEngine;
using VContainer;

namespace Asteroids
{
    public class BulletFiringController : IPausable, ISceneTickable
    {
        private BulletFactory _bulletFactory;
        private BulletRegistry _bulletRegistry;
        private float _bulletCooldown;
        private bool _isFiring;
        private float _nextShotTime;
        private Player _player;

        [Inject]
        public void Construct(
            BulletFactory bulletFactory,
            BulletRegistry bulletRegistry,
            PlayerCombatSettings playerCombatSettings,
            Player player)
        {
            _bulletFactory = bulletFactory;
            _bulletRegistry = bulletRegistry;
            _player = player;
            _bulletCooldown = playerCombatSettings.BulletCooldown;
            _nextShotTime = Time.time;
        }

        public void Pause()
        {
            _isFiring = false;
        }

        public void Unpause()
        {
        }

        public void Tick()
        {
            if (!_isFiring || Time.time < _nextShotTime)
            {
                return;
            }

            Bullet bullet = _bulletFactory.Create(_player.transform.position, _player.transform.rotation);
            _bulletRegistry.Register(bullet);
            _nextShotTime = Time.time + _bulletCooldown;
        }

        public void StartFiring()
        {
            _isFiring = true;
        }

        public void StopFiring()
        {
            _isFiring = false;
        }
    }
}
