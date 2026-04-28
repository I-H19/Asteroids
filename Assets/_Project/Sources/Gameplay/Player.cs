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
    public class Player : MonoBehaviour, ISceneTickable
    {
        public event Action Death;
        public ScreenBoundsTracker ScreenBoundsTrackerTemplate { get; private set; }
        public Rigidbody2D RigidBodyTemplate { get; private set; }
        public InertialMover InertialMoverTemplate { get; private set; }
        public DirectionalRotator DirectionalRotatorTemplate { get; private set; }
        public Laser PlayerLaser { get; private set; }

        private ScreenTeleporter _playerTeleporter;
        private PlayerLife _life;
        private float _maxHealth;
        private Camera _camera;

        [Inject]
        public void Construct(PlayerCombatSettings combatSettings, Camera cameraTemplate)
        {
            _camera = cameraTemplate;
            _maxHealth = combatSettings.MaxHealth;
        }

        public void Init(Laser laser)
        {
            PlayerLaser = laser;

            _playerTeleporter = GetComponent<ScreenTeleporter>();

            RigidBodyTemplate = GetComponent<Rigidbody2D>();

            InertialMoverTemplate = GetComponent<InertialMover>();
            DirectionalRotatorTemplate = GetComponent<DirectionalRotator>();
            
            _life = GetComponent<PlayerLife>();
            _life.Init(_maxHealth);

            ScreenBoundsTrackerTemplate = new ScreenBoundsTracker();
            ScreenBoundsTrackerTemplate.Init(RigidBodyTemplate, _camera);

            _playerTeleporter.Init(ScreenBoundsTrackerTemplate);

            _life.OnDeath += OnDeath;
        }

        private void OnDeath()
        {
            Death?.Invoke();
        }

        public void Tick()
        {
            _playerTeleporter.Tick();
        }
        public void ResetPlayer()
        {
            RigidBodyTemplate.position = Vector3.zero;
            RigidBodyTemplate.rotation = 0;

            PlayerLaser.ResetParameters();
        }
        private void OnDestroy() => _life.OnDeath -= Death;
    }
}
