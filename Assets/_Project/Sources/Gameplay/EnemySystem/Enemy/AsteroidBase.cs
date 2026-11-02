using System;
using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

namespace Asteroids
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(DirectionalMover), typeof(Rigidbody2D), typeof(EnemyLife))]
    [RequireComponent(typeof(EnemyDamageSource), typeof(ScreenTeleporter))]
    public class AsteroidBase : MonoBehaviour
    {
        private EnemyLife _enemyLife;
        private ScreenBoundsTracker _screenBoundsTracker;
        private ScreenTeleporter _screenTeleporter;
        public DirectionalMover Mover { get; private set; }

        public event Action Died;

        [Inject]
        public void Construct(ScreenBoundsTracker screenBoundsTracker)
        {
            _screenBoundsTracker = screenBoundsTracker;
        }

        public void Init(DirectionalMoverSettings moverSettings, float damageCount)
        {
            float randomAngleDegrees = Random.Range(0f, 360f);
            transform.localEulerAngles = new(0f, 0f, randomAngleDegrees);

            Rigidbody2D rigidBody2D = GetComponent<Rigidbody2D>();
            Mover = GetComponent<DirectionalMover>();
            Mover.Init(moverSettings, rigidBody2D);

            GetComponent<EnemyDamageSource>().Init(damageCount);

            _screenTeleporter = GetComponent<ScreenTeleporter>();
            _screenBoundsTracker.Init(rigidBody2D);
            _screenTeleporter.Init(_screenBoundsTracker);

            _enemyLife = GetComponent<EnemyLife>();
            _enemyLife.Died += OnDied;
        }

        private void OnDestroy()
        {
            if (_enemyLife != null)
            {
                _enemyLife.Died -= OnDied;
            }
        }

        public void FixedTick()
        {
            Mover.Move();
            _screenTeleporter.FixedTick();
        }

        private void OnDied()
        {
            Died?.Invoke();
        }
    }
}