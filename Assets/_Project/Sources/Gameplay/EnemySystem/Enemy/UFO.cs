using System;
using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

namespace Asteroids
{
    [RequireComponent(typeof(DirectionalMover))]
    [RequireComponent(typeof(LookAtRotator))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(EnemyLife))]
    [RequireComponent(typeof(ScreenTeleporter))]
    [RequireComponent(typeof(EnemyDamageSource))]
    public class Ufo : MonoBehaviour, IEnemy
    {
        private DirectionalMover _directionalMover;
        private EnemyLife _enemyLife;
        private LookAtRotator _lookAtRotator;
        private Rigidbody2D _rigidbody2D;
        private ScreenBoundsTracker _screenBoundsTracker;
        private DirectionalMoverSettings _moverSettings;

        public IMover Mover => _directionalMover;
        public event Action<IEnemy> Killed;

        private void OnDestroy()
        {
            if (_enemyLife != null)
            {
                _enemyLife.Died -= OnDied;
            }
        }

        public void Kill()
        {
            Destroy(gameObject);
        }

        public void FixedTick()
        {
            _lookAtRotator.Rotate();
            _directionalMover.Move();
        }

        [Inject]
        public void Construct(ScreenBoundsTracker screenBoundsTracker)
        {
            _screenBoundsTracker = screenBoundsTracker;
        }

        public void Init(DirectionalMoverSettings moverSettings, Player player, float damageCount)
        {
            _moverSettings = moverSettings;

            float randomAngleDegrees = Random.Range(0f, 360f);
            gameObject.transform.rotation = Quaternion.Euler(0f, 0f, randomAngleDegrees);

            _enemyLife = GetComponent<EnemyLife>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _directionalMover = GetComponent<DirectionalMover>();
            _directionalMover.Init(_moverSettings, _rigidbody2D);
            _directionalMover.EnableMoving();

            EnemyDamageSource damageSource = GetComponent<EnemyDamageSource>();
            damageSource.Init(damageCount);

            _lookAtRotator = GetComponent<LookAtRotator>();
            _lookAtRotator.Init(player.transform);

            _screenBoundsTracker.Init(_rigidbody2D);

            _enemyLife.Died += OnDied;
        }

        private void OnDied()
        {
            Killed?.Invoke(this);
        }
    }
}
