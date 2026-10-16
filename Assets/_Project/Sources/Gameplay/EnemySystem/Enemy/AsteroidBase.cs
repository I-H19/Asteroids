using System;
using _Project.Sources.Config.Movement;
using _Project.Sources.Gameplay.DamageSystem.Damageable;
using _Project.Sources.Gameplay.DamageSystem.DamageSource;
using _Project.Sources.Gameplay.ObjectMovement;
using _Project.Sources.Gameplay.ObjectMovement.Movers;
using UnityEngine;
using VContainer;

namespace _Project.Sources.Gameplay.EnemySystem.Enemy
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(DirectionalMover), 
        typeof(Rigidbody2D), 
        typeof(EnemyLife))]
    [RequireComponent(typeof(EnemyDamageSource), 
        typeof(ScreenTeleporter))]
    public class AsteroidBase : MonoBehaviour
    {
        public event Action Died;
        public DirectionalMover Mover { get; private set; }

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

        public void Init(DirectionalMoverSettings moverSettings, float damageCount)
        {
            float randomAngleDegrees = UnityEngine.Random.Range(0f, 360f);
            transform.localEulerAngles = new Vector3(0f, 0f, randomAngleDegrees);

            Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
            Mover = GetComponent<DirectionalMover>();
            Mover.Init(moverSettings, rigidbody2D);

            GetComponent<EnemyDamageSource>().Init(damageCount);

            _screenTeleporter = GetComponent<ScreenTeleporter>();
            _screenBoundsTracker.Init(rigidbody2D, _camera);
            _screenTeleporter.Init(_screenBoundsTracker);

            _enemyLife = GetComponent<EnemyLife>();
            _enemyLife.OnDeath += OnDeath;
        }

        public void Tick()
        {
            Mover.Move();
            _screenTeleporter.Tick();
        }

        private void OnDeath() => Died?.Invoke();

        private void OnDestroy()
        {
            if (_enemyLife != null)
                _enemyLife.OnDeath -= OnDeath;
        }
    }
}
