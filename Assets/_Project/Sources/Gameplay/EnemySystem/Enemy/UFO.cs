using Asteroids.GameLoop;
using Asteroids.Gameplay.DamageSystem;
using Asteroids.Gameplay.ObjectMovement;
using Asteroids.Settings;
using System;
using UnityEngine;

namespace Asteroids.Gameplay.EnemySystem
{
    [RequireComponent(typeof(DirectionalMover))]
    [RequireComponent(typeof(LookAtRotator))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(EnemyLife))]
    [RequireComponent(typeof(ScreenTeleporter))]
    [RequireComponent(typeof(EnemyDamageSource))]
    public class UFO : MonoBehaviour, IEnemy, ISceneTickable
    {
        public GameObject EnemyGameObject { get; private set; }
        public DirectionalMoverSettings MoverSettings { get; private set; }

        public IMover Mover { get; private set; }

        public Action<IEnemy> Killed { get; set; }

        private EnemyLife _enemyLife;
        private Rigidbody2D _rigidbody2D;
        private LookAtRotator _lookAtRotator;

        public void Init(GameObject gameObject, DirectionalMoverSettings moverSettings, SceneObjectHolder sceneObjectHolder, float damageCount)
        {
            EnemyGameObject = gameObject;
            MoverSettings = moverSettings;

            float randomAngleDegrees = UnityEngine.Random.Range(0f, 360f);
            EnemyGameObject.transform.rotation = Quaternion.Euler(0f, 0f, randomAngleDegrees);

            _enemyLife = GetComponent<EnemyLife>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            Mover = GetComponent<DirectionalMover>();
            Mover.Init(MoverSettings, _rigidbody2D);
            Mover.SetMoving(true);

            EnemyDamageSource damageSource = GetComponent<EnemyDamageSource>();
            damageSource.Init(damageCount);

            _lookAtRotator = GetComponent<LookAtRotator>();
            _lookAtRotator.Init(sceneObjectHolder.Player.transform);

            _enemyLife.OnDeath += OnDeath;
        }

        private void OnDeath()
        {
            Killed?.Invoke(this);
        }
        public void Kill()
        {
            Destroy(EnemyGameObject);
        }

        public void Tick()
        {
            _lookAtRotator.Rotate();
            Mover.Move();
        }
    }
}
