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
    public class Player : MonoBehaviour, ISceneTickable, IPausable
    {
        public event Action Died;
        
        [field: SerializeField]
        public Laser PlayerLaser { get; private set; }

        [field: SerializeField]
        public LaserVisual PlayerLaserVisual { get; private set; }

        private Camera _camera;
        private PlayerLife _life;
        private float _maxHealth;
        private PlayerCombatSettings _playerCombatSettings;
        private ScreenTeleporter _playerTeleporter;

        private ScreenBoundsTracker _screenBoundsTracker;
        public Rigidbody2D PlayerRigidBody { get; private set; }
        public InertialMover PlayerInertialMover { get; private set; }
        public DirectionalRotator PlayerDirectionalRotator { get; private set; }

        private void OnDestroy()
        {
            _life.Died -= OnDied;
        }

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

        public void Tick()
        {
            _playerTeleporter.Tick();
            PlayerLaser.LocalTick();
        }


        [Inject]
        public void Construct(PlayerCombatSettings combatSettings, Camera mainCamera, ScreenBoundsTracker boundsTracker)
        {
            _camera = mainCamera;
            _maxHealth = combatSettings.MaxHealth;
            _playerCombatSettings = combatSettings;
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

            _life.Died += OnDied;

            PlayerLaser.Init(PlayerLaserVisual, _playerCombatSettings);
        }

        private void OnDied()
        {
            Died?.Invoke();
        }

        public void ResetPlayer()
        {
            PlayerRigidBody.position = Vector3.zero;
            PlayerRigidBody.rotation = 0;

            PlayerLaser.ResetParameters();
        }
    }
}