using System;
using System.Collections.Generic;
using _Project.Sources.Config.Movement;
using _Project.Sources.Gameplay.DamageSystem.Damageable;
using _Project.Sources.Gameplay.DamageSystem.DamageSource;
using _Project.Sources.Gameplay.ObjectMovement;
using _Project.Sources.Gameplay.ObjectMovement.Movers;
using UnityEngine;
using VContainer;

namespace _Project.Sources.Gameplay.EnemySystem.Enemy
{
    [RequireComponent(typeof(DirectionalMover))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(EnemyLife))]
    [RequireComponent(typeof(EnemyDamageSource))]
    public class Asteroid : MonoBehaviour, IEnemy
    {
        public event Action<IEnemy> Killed;
        public DirectionalMoverSettings MoverSettings { get; private set; }
        public IMover Mover => _directionalMover;
        [field: SerializeField] public bool IsActiveFragment { get; private set; } = false;
        [field: SerializeField] public List<AsteroidFragment> Fragments { get; private set; } = new();

        private DirectionalMover _directionalMover;
        private Rigidbody2D _rigidbody2D;
        private EnemyLife _enemyLife;
        private ScreenTeleporter _screenTeleporter;
        private ScreenBoundsTracker _screenBoundsTracker;
        private Camera _camera;


        [Inject]
        public void Construct(Camera mainCamera, ScreenBoundsTracker screenBoundsTracker)
        {
            _camera = mainCamera;
            _screenBoundsTracker = screenBoundsTracker;
        }

        public void Init(
            DirectionalMoverSettings moverSettings,
            float damageCount)
        {
            float randomAngleDegrees = UnityEngine.Random.Range(0f, 360f);
            gameObject.transform.localEulerAngles = new Vector3(0f, 0f, randomAngleDegrees);

            MoverSettings = moverSettings;

            _enemyLife = GetComponent<EnemyLife>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _directionalMover = GetComponent<DirectionalMover>();
            _directionalMover.Init(MoverSettings, _rigidbody2D);

            _directionalMover.EnableMoving();

            EnemyDamageSource damageSource = GetComponent<EnemyDamageSource>();
            damageSource.Init(damageCount);


            _screenTeleporter = GetComponent<ScreenTeleporter>();

            _screenBoundsTracker.Init(_rigidbody2D, _camera);

            _screenTeleporter.Init(_screenBoundsTracker);

            _enemyLife.OnDeath += OnDeath;
        }

        public void AddFragment(AsteroidFragment fragment) => Fragments.Add(fragment);

        public void Tick()
        {
            _directionalMover.Move();
            _screenTeleporter.Tick();
        }

        public void Kill()
        {
            ActivateFragments();
            Destroy(gameObject);
        }

        private void ActivateFragments()
        {
            foreach (AsteroidFragment fragment in Fragments)
                fragment.Activate();
        }

        private void OnDeath() => Killed?.Invoke(this);
    }
}