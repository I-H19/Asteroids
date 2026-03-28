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
        public IMover Mover { get; private set; }
        [field: SerializeField] public bool IsFragment { get; private set; } = false;
        [field: SerializeField] public bool IsActiveFragment { get; private set; } = false;
        [field: SerializeField] public List<AsteroidFragment> Fragments { get; private set; } = new();

        private Rigidbody2D _rigidbody2D;
        private EnemyLife _enemyLife;
        private ScreenTeleporter _screenTeleporter;
        private ScreenBoundsTracker _screenBoundsTracker;
        private Camera _camera;


        [Inject]
        public void Construct(Camera mainCamera) => _camera = mainCamera;
        public void Init(DirectionalMoverSettings moverSettings, float damageCount, bool isFragment)
        {
            float randomAngleDegrees = UnityEngine.Random.Range(0f, 360f);
            gameObject.transform.localEulerAngles = new Vector3(0f, 0f, randomAngleDegrees);

            MoverSettings = moverSettings;

            _enemyLife = GetComponent<EnemyLife>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            Mover = GetComponent<DirectionalMover>();
            Mover.Init(MoverSettings, _rigidbody2D);

            if (IsFragment) Mover.SetEnabled(false);
            else Mover.SetMoving(true);

            EnemyDamageSource damageSource = GetComponent<EnemyDamageSource>();
            damageSource.Init(damageCount);


            _screenTeleporter = GetComponent<ScreenTeleporter>();

            _screenBoundsTracker = new ScreenBoundsTracker();
            _screenBoundsTracker.Init(_rigidbody2D, _camera);

            _screenTeleporter.Init(_screenBoundsTracker);
            
            _enemyLife.OnDeath += OnDeath;
        }

        public void ActivateFragments()
        {
            if (!IsFragment)
            {
                foreach (AsteroidFragment fragment in Fragments)
                    fragment.Activate();
            }
        }

        public void AddFragment(AsteroidFragment fragmentGameObject)
        {
            AsteroidFragment fragment = fragmentGameObject.GetComponent<AsteroidFragment>();
            Fragments.Add(fragment);
        }

        public void Tick()
        {
            Mover.Move();
            _screenTeleporter.Tick();
        }

        public void Kill() => Destroy(gameObject);
        private void OnDeath() => Killed?.Invoke(this);
    }
}