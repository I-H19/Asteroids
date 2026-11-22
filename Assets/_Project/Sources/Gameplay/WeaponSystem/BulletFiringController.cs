using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Asteroids
{
    public class BulletFiringController : IPausable, IDisposable
    {
        private BulletFactory _bulletFactory;
        private BulletRegistry _bulletRegistry;
        private float _bulletCooldown;
        private bool _isFiring;
        private Player _player;
        private UniTask _firingTask;

        private CancellationTokenSource _cancellationTokenSource;

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
        }

        public void Pause()
        {
            StopFiring();
        }

        public void Unpause() { }


        public void StartFiring()
        {
            _isFiring = true;

            if (_firingTask.Status == UniTaskStatus.Pending)
            {
                return;
            }
            
            if (_cancellationTokenSource == null)
            {
                _cancellationTokenSource = new();
            }
            
            _firingTask = FiringLoop().Preserve();
            _firingTask.Forget();
            
        }

        public void StopFiring()
        {
            _isFiring = false;
        }

        private async UniTask FiringLoop()
        {
            while (!_cancellationTokenSource.IsCancellationRequested)
            {
                if (!_isFiring)
                {
                    await UniTask.Yield(_cancellationTokenSource.Token);
                    continue;
                }

                Bullet bullet = _bulletFactory.Create(_player.transform.position, _player.transform.rotation);
                _bulletRegistry.Register(bullet);

                await UniTask.WaitForSeconds(_bulletCooldown, cancellationToken: _cancellationTokenSource.Token);
            }
        }

        public void Dispose()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }
    }
}