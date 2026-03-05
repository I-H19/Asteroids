using UnityEngine;
using VContainer;

[RequireComponent(typeof(PlayerDamageSource))]
public class Laser : MonoBehaviour
{
    private PlayerDamageSource _damageSource;
    private GameObject _laserVisual;

    public int MaxChargesNumber { get; private set; }

    private int _startCharges;

    public int TotalAmmo { get; private set; } = 4;
    public float ReloadingTime { get; private set; } = 0;
    public float ShootingCooldown { get; private set; } = 0;

    private float _operatingTime;
    private float _endOfOperatingTime;

    private float _reloadingChargeTime;
    private float _shootingCooldown;
    private float _nextChargeTime;
    private float _nextShootTime;

    private bool _isShooting = false;


    [Inject]
    public void Construct(PlayerCombatSettings combatSettings, SceneObjectHolder sceneObjectHolder)
    {
        _laserVisual = sceneObjectHolder.LaserVisual;
        _damageSource = GetComponent<PlayerDamageSource>();

        _damageSource.ChangeDamage(combatSettings.LaserDamage);
        _damageSource.ChangeType(true);

        SetLaserActive(false);


        MaxChargesNumber = combatSettings.MaxLaserCharge;
        
        _startCharges = combatSettings.StartLaserCharges;

        _reloadingChargeTime = combatSettings.LaserChargeReloadingTime;
        _shootingCooldown = combatSettings.LaserShootCooldown;
        _operatingTime = combatSettings.LaserOperatingTime;

        _nextShootTime = Time.time;
        _nextChargeTime = Time.time;
    }

    public void TryShoot()
    {
        if (Time.time <= _nextShootTime || TotalAmmo == 0 || _isShooting) return;

        SetLaserActive(true);
        TotalAmmo--;
        _nextShootTime = Time.time + _shootingCooldown;
    }
    public void ResetParameters()
    {
        SetLaserActive(false);

        TotalAmmo = _startCharges;
        _nextShootTime = Time.time;
        _nextChargeTime = Time.time;
    }
    public void Tick()
    {
        if (_isShooting) _damageSource.Tick();

        float reloadingTime = _nextChargeTime - Time.time;
        float shootingCooldown = _nextShootTime - Time.time;

        if (shootingCooldown < 0) ShootingCooldown = 0;
        else ShootingCooldown = shootingCooldown;

        if (reloadingTime < 0) ReloadingTime = 0;
        else ReloadingTime = reloadingTime;

        if (_isShooting && Time.time > _endOfOperatingTime) SetLaserActive(false);

        if (TotalAmmo == MaxChargesNumber) return;
        if (TotalAmmo > MaxChargesNumber)
        {
            TotalAmmo = MaxChargesNumber;
            return;
        }

        if (Time.time > _nextChargeTime)
        {
            _nextChargeTime = Time.time + _reloadingChargeTime;
            TotalAmmo++;
        }

    }
    private void SetLaserActive(bool isActive)
    {
        if (isActive)
        {
            _isShooting = true;
            _damageSource.enabled = true;
            _laserVisual.SetActive(true);
            _endOfOperatingTime = Time.time + _operatingTime;
        }
        else
        {
            _isShooting = false;
            _damageSource.enabled = false;
            _laserVisual.SetActive(false);
        }
    }

}
