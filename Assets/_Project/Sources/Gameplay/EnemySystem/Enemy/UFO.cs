using System;
using _Project.Sources.Config.Movement;
using _Project.Sources.GameLoop;
using _Project.Sources.Gameplay.DamageSystem.Damageable;
using _Project.Sources.Gameplay.DamageSystem.DamageSource;
using _Project.Sources.Gameplay.ObjectMovement;
using _Project.Sources.Gameplay.ObjectMovement.Movers;
using _Project.Sources.Gameplay.ObjectMovement.Rotators;
using UnityEngine;
using VContainer;

namespace _Project.Sources.Gameplay.EnemySystem.Enemy
{
    [RequireComponent(typeof(DirectionalMover))]
    [RequireComponent(typeof(LookAtRotator))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(EnemyLife))]
    [RequireComponent(typeof(ScreenTeleporter))]
    [RequireComponent(typeof(EnemyDamageSource))]
    public class UFO : MonoBehaviour, IEnemy, ISceneTickable
    {
        public DirectionalMoverSettings MoverSettings { get; private set; }

        public IMover Mover { get; private set; }

        public event Action<IEnemy> Killed;

        private EnemyLife _enemyLife;
        private Rigidbody2D _rigidbody2D;
        private LookAtRotator _lookAtRotator;
        private ScreenBoundsTracker _screenBoundsTracker;
        private Camera _camera;

        [Inject]
        public void Construct(Camera mainCamera) => _camera = mainCamera;
        public void Init(DirectionalMoverSettings moverSettings, Player player, float damageCount)
        {
            MoverSettings = moverSettings;

            float randomAngleDegrees = UnityEngine.Random.Range(0f, 360f);
            gameObject.transform.rotation = Quaternion.Euler(0f, 0f, randomAngleDegrees);

            _enemyLife = GetComponent<EnemyLife>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            Mover = GetComponent<DirectionalMover>();
            Mover.Init(MoverSettings, _rigidbody2D);
            Mover.SetMoving(true);

            EnemyDamageSource damageSource = GetComponent<EnemyDamageSource>();
            damageSource.Init(damageCount);

            _lookAtRotator = GetComponent<LookAtRotator>();
            _lookAtRotator.Init(player.transform);

            _screenBoundsTracker = new ScreenBoundsTracker();
            _screenBoundsTracker.Init(_rigidbody2D, _camera);
            
            _enemyLife.OnDeath += OnDeath;
        }

        private void OnDeath()
        {
            Killed?.Invoke(this);
        }
        public void Kill()
        {
            Destroy(gameObject);
        }

        public void Tick()
        {
            _lookAtRotator.Rotate();
            Mover.Move();
        }
    }
}
