using UnityEngine;
using VContainer;

[RequireComponent(typeof(PlayerDamageSource))]
public class Laser : MonoBehaviour
{
    private PlayerDamageSource _damageSource;
    private GameObject _laserVisual;

    public int MaxChargesNumber { get; private set; }
    public int ChargesCount { get; private set; } = 4;

    private float _operatingTime;
    private float _endOfOperatingTime;

    private float _reloadingChargeTime;
    private float _shootingCooldown;
    private float _nextChargeTime;
    private float _nextShootTime;

    private bool _isShooting = false;


    [Inject]
    public void Construct(PlayerCombatSettings playerCombatSettings, SceneObjectHolder sceneObjectHolder)
    {
        _laserVisual = sceneObjectHolder.LaserVisual;
        _damageSource = GetComponent<PlayerDamageSource>();

        _damageSource.ChangeDamage(playerCombatSettings.LaserDamage);
        _damageSource.ChangeType(true);

        SetLaserActive(false);

        MaxChargesNumber = playerCombatSettings.MaxLaserCharge;
        _reloadingChargeTime = playerCombatSettings.LaserChargeReloadingTime;
        _shootingCooldown = playerCombatSettings.LaserShootCooldown;
        _operatingTime = playerCombatSettings.LaserOperatingTime;

        _nextShootTime = Time.time;
        _nextChargeTime = Time.time;
    }

    public void TryShoot()
    {
        if (Time.time <= _nextShootTime || ChargesCount == 0 || _isShooting) return;
        
        SetLaserActive(true);
        ChargesCount--;
        _nextShootTime = Time.time + _shootingCooldown;
    }
    private void Update()
    {        
        if(_isShooting && Time.time > _endOfOperatingTime) SetLaserActive(false);

        if (ChargesCount == MaxChargesNumber) return;
        if (ChargesCount > MaxChargesNumber)
        {
            ChargesCount = MaxChargesNumber;
            return;
        }

        if(Time.time > _nextChargeTime)
        {
            _nextChargeTime = Time.time + _reloadingChargeTime;
            ChargesCount++;
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
