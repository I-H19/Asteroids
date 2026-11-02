using System;
using UnityEngine;
using VContainer;

namespace Asteroids
{
    [RequireComponent(typeof(ScreenTeleporter))]
    [RequireComponent(typeof(InertialMover))]
    [RequireComponent(typeof(DirectionalRotator))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PlayerLife))]
    public class Player : MonoBehaviour, ISceneTickable, ISceneFixedTickable, IPausable
    {
        [field: SerializeField]
        private Laser PlayerLaser { get; set; }

        [field: SerializeField]
        private LaserVisual PlayerLaserVisual { get; set; }

        private DirectionalRotator _directionalRotator;
        private InertialMover _inertialMover;
        private PlayerLife _life;
        private float _maxHealth;
        private PlayerCombatSettings _playerCombatSettings;
        private PlayerMovementSettings _playerMovementSettings;
        private ScreenTeleporter _playerTeleporter;
        private Rigidbody2D _rigidbody;

        private ScreenBoundsTracker _screenBoundsTracker;
        public float CurrentAngle => _directionalRotator.CurrentAngle;

        public float CurrentSpeed => _inertialMover.CurrentSpeed;
        public float LaserReloadingTime => PlayerLaser.ReloadingTime;
        public float LaserShootingCooldown => PlayerLaser.ShootingCooldown;
        public int MaxLaserCharges => PlayerLaser.MaxChargesNumber;
        public Vector2 Position => _rigidbody.position;
        public int TotalLaserAmmo => PlayerLaser.TotalAmmo;
        public event Action AngleChanged;

        public event Action Died;
        public event Action LaserAmmoChanged;
        public event Action LaserTicked;
        public event Action PositionChanged;
        public event Action SpeedChanged;

        [Inject]
        public void Construct(
            PlayerCombatSettings combatSettings,
            PlayerMovementSettings movementSettings,
            ScreenBoundsTracker boundsTracker)
        {
            _maxHealth = combatSettings.MaxHealth;
            _playerCombatSettings = combatSettings;
            _playerMovementSettings = movementSettings;
            _screenBoundsTracker = boundsTracker;
        }


        public void Init()
        {
            _playerTeleporter = GetComponent<ScreenTeleporter>();

            _rigidbody = GetComponent<Rigidbody2D>();

            _inertialMover = GetComponent<InertialMover>();
            _directionalRotator = GetComponent<DirectionalRotator>();

            _directionalRotator.Init(_rigidbody, _playerMovementSettings.RotationSettings);
            _inertialMover.Init(_playerMovementSettings.MoverSettings, _rigidbody);

            _life = GetComponent<PlayerLife>();
            _life.Init(_maxHealth);

            _screenBoundsTracker.Init(_rigidbody);

            _playerTeleporter.Init(_screenBoundsTracker);

            _life.Died += OnDied;
            _inertialMover.SpeedChanged += OnSpeedChanged;
            _inertialMover.PositionChanged += OnPositionChanged;
            _directionalRotator.AngleChanged += OnAngleChanged;
            PlayerLaser.AmmoChanged += OnLaserAmmoChanged;
            PlayerLaser.Ticked += OnLaserTicked;

            PlayerLaser.Init(PlayerLaserVisual, _playerCombatSettings);
        }

        private void OnDestroy()
        {
            _life.Died -= OnDied;
            _inertialMover.SpeedChanged -= OnSpeedChanged;
            _inertialMover.PositionChanged -= OnPositionChanged;
            _directionalRotator.AngleChanged -= OnAngleChanged;
            PlayerLaser.AmmoChanged -= OnLaserAmmoChanged;
            PlayerLaser.Ticked -= OnLaserTicked;
        }

        public void Pause()
        {
            _inertialMover.DisableMoving();
            _directionalRotator.SetEnabled(false);
        }

        public void Unpause()
        {
            _inertialMover.EnableMoving();
            _directionalRotator.SetEnabled(true);
        }

        public void FixedTick()
        {
            _inertialMover.Move();
            _directionalRotator.Rotate();
            _playerTeleporter.FixedTick();
            PlayerLaser.FixedTick();
        }

        public void Tick()
        {
            PlayerLaser.LocalTick();
        }

        public void ResetPlayer()
        {
            _life.RestoreHealth();

            _rigidbody.position = Vector2.zero;
            _rigidbody.rotation = 0;

            PlayerLaser.ResetParameters();
        }

        public void SetAccelerating(bool isAccelerating)
        {
            _inertialMover.SetAccelerating(isAccelerating);
        }

        public void SetBraking(bool isBraking)
        {
            _inertialMover.SetBraking(isBraking);
        }

        public void SetRotateLeft(bool isRotating)
        {
            _directionalRotator.SetRotateLeft(isRotating);
        }

        public void SetRotateRight(bool isRotating)
        {
            _directionalRotator.SetRotateRight(isRotating);
        }

        public void TryLaserShoot()
        {
            PlayerLaser.TryShoot();
        }

        private void OnDied()
        {
            Died?.Invoke();
        }

        private void OnSpeedChanged()
        {
            SpeedChanged?.Invoke();
        }

        private void OnPositionChanged()
        {
            PositionChanged?.Invoke();
        }

        private void OnAngleChanged()
        {
            AngleChanged?.Invoke();
        }

        private void OnLaserAmmoChanged()
        {
            LaserAmmoChanged?.Invoke();
        }

        private void OnLaserTicked()
        {
            LaserTicked?.Invoke();
        }
    }
}