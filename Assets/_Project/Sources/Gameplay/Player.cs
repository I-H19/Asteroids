using System;
using _Project.Sources.Config;
using _Project.Sources.GameLoop;
using _Project.Sources.Gameplay.DamageSystem.Damageable;
using _Project.Sources.Gameplay.ObjectMovement;
using _Project.Sources.Gameplay.ObjectMovement.Movers;
using _Project.Sources.Gameplay.ObjectMovement.Rotators;
using _Project.Sources.Gameplay.WeaponSystem;
using UnityEngine;
using VContainer;

namespace _Project.Sources.Gameplay
{
    [RequireComponent(typeof(ScreenTeleporter))]
    [RequireComponent(typeof(InertialMover))]
    [RequireComponent(typeof(DirectionalRotator))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PlayerLife))]
    public class Player : MonoBehaviour, ISceneTickable, IPausable
    {
        public event Action Death;
        public Rigidbody2D PlayerRigidBody { get; private set; }
        public InertialMover PlayerInertialMover { get; private set; }
        public DirectionalRotator PlayerDirectionalRotator { get; private set; }
        [field: SerializeField] public Laser PlayerLaser { get; private set; }
        [field: SerializeField] public LaserVisual PlayerLaserVisual { get; private set; }

        private ScreenBoundsTracker _screenBoundsTracker; 
        private ScreenTeleporter _playerTeleporter;
        private PlayerLife _life;
        private float _maxHealth;
        private Camera _camera;
        private PlayerCombatSettings _playerCombatSettings;

        [Inject]
        public void Construct(
            PlayerCombatSettings combatSettings, 
            Camera mainCamera,
            ScreenBoundsTracker boundsTracker)
        {
            _camera = mainCamera;
            _maxHealth = combatSettings.MaxHealth;
            _playerCombatSettings =  combatSettings;
            _screenBoundsTracker = boundsTracker;
        }

        public void Init()
        {
            _playerTeleporter = GetComponent<ScreenTeleporter>();

            PlayerRigidBody = GetComponent<Rigidbody2D>();

            PlayerInertialMover = GetComponent<InertialMover>();
            PlayerDirectionalRotator = GetComponent<DirectionalRotator>();
            
            _life = GetComponent<PlayerLife>();
            _life.Init(_maxHealth);

            _screenBoundsTracker.Init(PlayerRigidBody, _camera);

            _playerTeleporter.Init(_screenBoundsTracker);

            _life.OnDeath += OnDeath;
            
            PlayerLaser.Init(PlayerLaserVisual, _playerCombatSettings);
        }

        private void OnDeath()
        {
            Death?.Invoke();
        }

        public void Tick()
        {
            _playerTeleporter.Tick();
            PlayerLaser.LocalTick();
        }
        public void ResetPlayer()
        {
            PlayerRigidBody.position = Vector3.zero;
            PlayerRigidBody.rotation = 0;

            PlayerLaser.ResetParameters();
        }
        private void OnDestroy() => _life.OnDeath -= Death;
        public void Pause()
        {
            PlayerInertialMover.DisableMoving();
            PlayerDirectionalRotator.SetEnabled(false);
        }

        public void Unpause()
        {
            PlayerInertialMover.EnableMoving();
            PlayerDirectionalRotator.SetEnabled(true);
        }
    }
}
