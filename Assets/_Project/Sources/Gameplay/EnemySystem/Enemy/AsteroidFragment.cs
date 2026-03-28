using System;
using _Project.Sources.Config.Movement;
using _Project.Sources.GameLoop;
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
    public class AsteroidFragment : MonoBehaviour, IEnemy
    {
        private Asteroid _parentAsteroid;
        public event Action<IEnemy> Killed;
        public IMover Mover { get; private set; }

        private DirectionalMoverSettings _moverSettings;
        private Rigidbody2D _rigidbody2D;
        private EnemyLife _enemyLife;
        private ScreenTeleporter _screenTeleporter;
        private ScreenBoundsTracker _screenBoundsTracker;
        private Camera _camera;

        [Inject]
        public void Construct(Camera mainCamera) => _camera = mainCamera;
        public void Init(DirectionalMoverSettings moverSettings, float damageCount)
        {
            _moverSettings = moverSettings;

            float randomAngleDegrees = UnityEngine.Random.Range(0f, 360f);
            gameObject.transform.localEulerAngles = new Vector3(0f, 0f, randomAngleDegrees);

            _enemyLife = GetComponent<EnemyLife>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            Mover = GetComponent<DirectionalMover>();
            Mover.Init(_moverSettings, _rigidbody2D);

            Mover.SetEnabled(false);

            EnemyDamageSource damageSource = GetComponent<EnemyDamageSource>();
            damageSource.Init(damageCount);

            _screenTeleporter = GetComponent<ScreenTeleporter>();

            _screenBoundsTracker = new ScreenBoundsTracker();
            _screenBoundsTracker.Init(_rigidbody2D, _camera);
            
            _screenTeleporter.Init(_screenBoundsTracker);

            _enemyLife.OnDeath += OnDeath;
        }

        public void Activate()
        {
            gameObject.SetActive(true);
            Mover.SetPosition(_parentAsteroid.transform.position);
            Mover.SetEnabled(true);
            Mover.SetMoving(true);
        }

        public void SetParent(Asteroid parentAsteroid) => _parentAsteroid = parentAsteroid;

        public void Tick()
        {
            Mover.Move();
            _screenTeleporter.Tick();
        }

        public void Kill()
        {
            Destroy(gameObject);
        }

        private void OnDeath()
        {
            Killed?.Invoke(this);
        }
    }
}