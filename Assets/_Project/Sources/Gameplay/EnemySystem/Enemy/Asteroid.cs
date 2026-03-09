using Asteroids.GameLoop;
using Asteroids.Gameplay.DamageSystem;
using Asteroids.Gameplay.ObjectMovement;
using Asteroids.Settings;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Gameplay.EnemySystem
{
    [RequireComponent(typeof(DirectionalMover))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(EnemyLife))]
    [RequireComponent(typeof(EnemyDamageSource))]
    public class Asteroid : MonoBehaviour, IEnemy, ISceneTickable
    {
        private Asteroid _parentAsteroid;
        public Action<IEnemy> Killed { get; set; }
        public GameObject EnemyGameObject { get; private set; }
        public DirectionalMoverSettings MoverSettings { get; private set; }
        public IMover Mover { get; private set; }
        [field: SerializeField] public bool IsFragment { get; private set; } = false;
        [field: SerializeField] public bool IsActiveFragment { get; private set; } = false;
        [field: SerializeField] public List<Asteroid> Fragments { get; private set; } = new();

        private Rigidbody2D _rigidbody2D;
        private EnemyLife _enemyLife;
        private ScreenTeleporter _screenTeleporter;

        public void Init(GameObject gameObject, DirectionalMoverSettings moverSettings, float damageCount, bool isFragment)
        {
            IsFragment = isFragment;

            EnemyGameObject = gameObject;
            MoverSettings = moverSettings;

            float randomAngleDegrees = UnityEngine.Random.Range(0f, 360f);
            EnemyGameObject.transform.rotation = Quaternion.Euler(0f, 0f, randomAngleDegrees);

            _enemyLife = GetComponent<EnemyLife>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            Mover = GetComponent<DirectionalMover>();
            Mover.Init(MoverSettings, _rigidbody2D);

            if (IsFragment) Mover.SetEnabled(false);
            else Mover.SetMoving(true);

            EnemyDamageSource damageSource = GetComponent<EnemyDamageSource>();
            damageSource.Init(damageCount);

            _enemyLife.OnDeath += OnDeath;

            _screenTeleporter = GetComponent<ScreenTeleporter>();
            _screenTeleporter.Init();
        }

        public void ActivateFragments()
        {
            if (!IsFragment)
            {
                foreach (Asteroid fragment in Fragments)
                {
                    fragment.gameObject.SetActive(true);
                    fragment.IsActiveFragment = true;
                    fragment.Mover.SetPosition(_rigidbody2D.position);
                    fragment.Mover.SetEnabled(true);
                    fragment.Mover.SetMoving(true);
                }
            }
        }
        public void SetParent(Asteroid parentAsteroid) => _parentAsteroid = parentAsteroid;
        public void AddFragment(GameObject fragmentGameObject)
        {
            Asteroid asteroid = fragmentGameObject.GetComponent<Asteroid>();
            Fragments.Add(asteroid);
        }

        public void Tick()
        {
            Mover.Move();
            _screenTeleporter.Tick();
        }

        public void Kill()
        {
            Destroy(EnemyGameObject);
        }
        private void OnDeath()
        {
            Killed?.Invoke(this);
        }
    }
}