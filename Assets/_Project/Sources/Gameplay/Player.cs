using System;
using _Project.Sources.GameLoop;
using _Project.Sources.Gameplay.DamageSystem.Damageable;
using _Project.Sources.Gameplay.ObjectMovement;
using _Project.Sources.Gameplay.ObjectMovement.Movers;
using _Project.Sources.Gameplay.ObjectMovement.Rotators;
using _Project.Sources.Gameplay.WeaponSystem;
using _Project.Sources.Settings;    
using UnityEngine;
using VContainer;

namespace _Project.Sources.Gameplay
{
    [RequireComponent(typeof(ScreenTeleporter))]
    [RequireComponent(typeof(ScreenBoundsTracker))]
    [RequireComponent(typeof(InertialMover))]
    [RequireComponent(typeof(DirectionalRotator))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PlayerLife))]
    public class Player : MonoBehaviour, ISceneTickable
    {
        public Action Death;
        public ScreenBoundsTracker ScreenBoundsTrackerTemplate { get; private set; }
        public Rigidbody2D RigidBodyTemplate { get; private set; }
        public InertialMover InertialMoverTemplate { get; private set; }
        public DirectionalRotator DirectionalRotatorTemplate { get; private set; }

        private ScreenTeleporter _playerTeleporter;
        private IObjectResolver _resolver;
        private Laser _laser;
        private PlayerLife _life;
        private float _maxHealth;

        [Inject]
        public void Construct(IObjectResolver resolver, PlayerCombatSettings combatSettings, Laser laser)
        {
            _resolver = resolver;
            _laser = laser;
            _maxHealth = combatSettings.MaxHealth;
        }

        public void Init()
        {
            _playerTeleporter = GetComponent<ScreenTeleporter>();

            ScreenBoundsTrackerTemplate = GetComponent<ScreenBoundsTracker>();
            RigidBodyTemplate = GetComponent<Rigidbody2D>();

            InertialMoverTemplate = GetComponent<InertialMover>();
            DirectionalRotatorTemplate = GetComponent<DirectionalRotator>();

            _resolver.Inject(ScreenBoundsTrackerTemplate);

            _playerTeleporter.Init();

            _life = GetComponent<PlayerLife>();
            _life.Init(_maxHealth);
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

            _laser.ResetParameters();
        }
        private void OnDestroy() => _life.OnDeath -= Death;
    }
}
