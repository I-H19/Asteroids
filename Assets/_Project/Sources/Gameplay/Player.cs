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
        public event Action Died;
        
        [field: SerializeField]
        public Laser PlayerLaser { get; private set; }

        [field: SerializeField]
        public LaserVisual PlayerLaserVisual { get; private set; }

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
            PlayerLaser.LocalTick();
        }

        public void FixedTick()
        {
            _playerTeleporter.FixedTick();
            PlayerLaser.FixedTick();
        }


        [Inject]
        public void Construct(PlayerCombatSettings combatSettings, ScreenBoundsTracker boundsTracker)
        {
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

            _screenBoundsTracker.Init(PlayerRigidBody);

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
