using System;
using UnityEngine;

namespace Asteroids
{
    [RequireComponent(typeof(PlayerDamageSource))]
    public class Laser : MonoBehaviour
    {
        private PlayerDamageSource _damageSource;
        private float _endOfOperatingTime;

        private bool _isShooting;
        private LaserVisual _laserVisual;
        private float _nextChargeTime;
        private float _nextShootTime;

        private float _operatingTime;

        private float _reloadingChargeTime;
        private float _shootingCooldown;

        private int _startCharges;

        public int MaxChargesNumber { get; private set; }
        public float ReloadingTime { get; private set; }
        public float ShootingCooldown { get; private set; }

        public int TotalAmmo { get; private set; }
        public event Action AmmoChanged;
        public event Action Ticked;


        public void Init(LaserVisual laserVisual, PlayerCombatSettings combatSettings)
        {
            _damageSource = GetComponent<PlayerDamageSource>();

            _damageSource.ChangeDamage(combatSettings.LaserDamage);
            _damageSource.ChangeHitType(HitDetectionType.HitAll);


            MaxChargesNumber = combatSettings.MaxLaserCharge;

            _startCharges = combatSettings.StartLaserCharges;

            _reloadingChargeTime = combatSettings.LaserChargeReloadingTime;
            _shootingCooldown = combatSettings.LaserShootCooldown;
            _operatingTime = combatSettings.LaserOperatingTime;

            _nextShootTime = Time.time;
            _nextChargeTime = Time.time;

            _laserVisual = laserVisual;
            SetLaserActive(false);
        }

        public void TryShoot()
        {
            if (Time.time <= _nextShootTime || TotalAmmo == 0 || _isShooting)
            {
                return;
            }

            SetLaserActive(true);
            TotalAmmo--;
            AmmoChanged?.Invoke();
            _nextShootTime = Time.time + _shootingCooldown;
        }

        public void ResetParameters()
        {
            SetLaserActive(false);

            TotalAmmo = _startCharges;
            AmmoChanged?.Invoke();
            _nextShootTime = Time.time;
            _nextChargeTime = Time.time;
        }

        public void LocalTick()
        {
            Ticked?.Invoke();

            float reloadingTime = _nextChargeTime - Time.time;
            float shootingCooldown = _nextShootTime - Time.time;

            if (shootingCooldown < 0)
            {
                ShootingCooldown = 0;
            }
            else
            {
                ShootingCooldown = shootingCooldown;
            }

            if (reloadingTime < 0)
            {
                ReloadingTime = 0;
            }
            else
            {
                ReloadingTime = reloadingTime;
            }

            if (_isShooting && Time.time > _endOfOperatingTime)
            {
                SetLaserActive(false);
            }

            if (TotalAmmo == MaxChargesNumber)
            {
                return;
            }

            if (TotalAmmo > MaxChargesNumber)
            {
                TotalAmmo = MaxChargesNumber;
                return;
            }

            if (!(Time.time > _nextChargeTime))
            {
                return;
            }

            _nextChargeTime = Time.time + _reloadingChargeTime;
            TotalAmmo++;
            AmmoChanged?.Invoke();
        }

        public void FixedTick()
        {
            if (_isShooting)
            {
                _damageSource.Tick();
            }
        }

        private void SetLaserActive(bool isActive)
        {
            if (isActive)
            {
                _isShooting = true;
                _damageSource.enabled = true;
                _laserVisual.Show();
                _endOfOperatingTime = Time.time + _operatingTime;
            }
            else
            {
                _isShooting = false;
                _damageSource.enabled = false;
                _laserVisual.Hide();
            }
        }
    }
}