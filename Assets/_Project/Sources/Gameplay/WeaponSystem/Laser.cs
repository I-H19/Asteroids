using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Asteroids
{
    [RequireComponent(typeof(PlayerDamageSource))]
    public class Laser : MonoBehaviour
    {
        public event Action AmmoChanged;
        public event Action LaserCooldownChanged;

        private CancellationTokenSource _firingLoopCancellationTokenSource;

        private PlayerDamageSource _damageSource;
        private Player _player;

        private bool _isShooting;
        private LaserVisual _laserVisual;
        private float _nextChargeTime;
        private float _nextShootTime;

        private float _operatingTime;

        private float _reloadingChargeTime;
        private float _tunedShootingCooldown;
        private float _shootingCooldown;

        private int _startCharges;
        private int _totalAmmo;
        private UniTask _firingLoopOperation;

        public int MaxChargesNumber { get; private set; }
        public float ReloadingTime { get; private set; }

        public float ShootingCooldown
        {
            get => _shootingCooldown;
            private set
            {
                _shootingCooldown = value;
                LaserCooldownChanged?.Invoke();
            }
        }

        public int TotalAmmo
        {
            get => _totalAmmo;
            private set
            {
                _totalAmmo = value;
                AmmoChanged?.Invoke();
            }
        }

        public void Init(Player player, LaserVisual laserVisual, PlayerCombatSettings combatSettings)
        {
            _damageSource = GetComponent<PlayerDamageSource>();

            _damageSource.ChangeDamage(combatSettings.LaserDamage);
            _damageSource.ChangeHitType(HitDetectionType.HitAll);


            MaxChargesNumber = combatSettings.MaxLaserCharge;

            _startCharges = combatSettings.StartLaserCharges;

            _reloadingChargeTime = combatSettings.LaserChargeReloadingTime;
            _tunedShootingCooldown = combatSettings.LaserShootCooldown;
            _operatingTime = combatSettings.LaserOperatingTime;

            _nextShootTime = Time.time;
            _nextChargeTime = Time.time;

            _laserVisual = laserVisual;
            SetLaserActive(false);

            _player = player;
            _player.Paused += StopFiringLoop;
            _player.Unpaused += StartFiringLoop;

            ConfigureLaserParameters();
            StartFiringLoop();
        }

        private void StartFiringLoop()
        {
            if (_firingLoopCancellationTokenSource == null ||
                _firingLoopCancellationTokenSource.IsCancellationRequested)
            {
                _firingLoopCancellationTokenSource = new();
            }

            if (_firingLoopOperation.Status != UniTaskStatus.Pending)
            {
                _firingLoopOperation = FiringLoop().Preserve();
                _firingLoopOperation.Forget();
            }
        }

        private void StopFiringLoop()
        {
            if (_firingLoopOperation.Status == UniTaskStatus.Pending)
            {
                _firingLoopCancellationTokenSource.Cancel();
                _firingLoopCancellationTokenSource.Dispose();
                _firingLoopCancellationTokenSource = null;
                SetLaserActive(false);
            }
        }

        public void TryShoot()
        {
            if (Time.time <= _nextShootTime || TotalAmmo == 0 || _isShooting)
            {
                return;
            }

            FireOperation().Forget();
        }

        public void ResetParameters()
        {
            StopFiringLoop();
            ConfigureLaserParameters();
        }

        private void ConfigureLaserParameters()
        {
            TotalAmmo = _startCharges;
            _nextShootTime = Time.time;
            _nextChargeTime = Time.time;
        }

        private void OnDestroy()
        {
            if (_player != null)
            {
                _player.Paused -= StopFiringLoop;
                _player.Unpaused -= StartFiringLoop;
            }

            _firingLoopCancellationTokenSource?.Cancel();
            _firingLoopCancellationTokenSource?.Dispose();
        }

        public void FixedTick()
        {
            if (_isShooting)
            {
                _damageSource.Tick();
            }
        }

        private void HandleReloadingTime()
        {
            float reloadingTime = _nextChargeTime - Time.time;

            if (reloadingTime < 0)
            {
                ReloadingTime = 0;
            }
            else
            {
                ReloadingTime = reloadingTime;
            }
        }

        private void HandleShootingCooldown()
        {
            float shootingCooldown = _nextShootTime - Time.time;

            if (shootingCooldown < 0)
            {
                ShootingCooldown = 0;
            }
            else
            {
                ShootingCooldown = _nextShootTime - Time.time;
            }
        }

        private void SetLaserActive(bool isActive)
        {
            if (isActive)
            {
                _isShooting = true;
                _damageSource.enabled = true;
                _laserVisual.Show();
            }
            else
            {
                _isShooting = false;
                _damageSource.enabled = false;
                _laserVisual.Hide();
            }
        }

        private async UniTask FiringLoop()
        {
            while (!_firingLoopCancellationTokenSource.Token.IsCancellationRequested)
            {
                HandleReloadingTime();
                HandleShootingCooldown();

                if (TotalAmmo == MaxChargesNumber)
                {
                    await UniTask.Yield(_firingLoopCancellationTokenSource.Token);
                    continue;
                }

                if (TotalAmmo > MaxChargesNumber)
                {
                    TotalAmmo = MaxChargesNumber;
                    await UniTask.Yield(_firingLoopCancellationTokenSource.Token);
                    continue;
                }

                if (!(Time.time > _nextChargeTime))
                {
                    await UniTask.Yield(_firingLoopCancellationTokenSource.Token);
                    continue;
                }

                _nextChargeTime = Time.time + _reloadingChargeTime;
                TotalAmmo++;

                await UniTask.WaitForFixedUpdate(_firingLoopCancellationTokenSource.Token);
            }
        }

        private async UniTask FireOperation()
        {
            SetLaserActive(true);
            TotalAmmo--;
            _nextShootTime = Time.time + _tunedShootingCooldown;
            await UniTask.WaitForSeconds(_operatingTime, cancellationToken: _firingLoopCancellationTokenSource.Token);
            SetLaserActive(false);
        }
    }
}