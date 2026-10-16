using System;
using System.Collections.Generic;
using _Project.Sources.Config.Movement;
using _Project.Sources.Gameplay.ObjectMovement.Movers;
using UnityEngine;

namespace _Project.Sources.Gameplay.EnemySystem.Enemy
{
    [RequireComponent(typeof(AsteroidBase))]
    public class Asteroid : MonoBehaviour, IEnemy
    {
        [field: SerializeField] public List<AsteroidFragment> Fragments { get; private set; } = new();

        public event Action<IEnemy> Killed;
        public IMover Mover => _asteroidBase.Mover;

        private AsteroidBase _asteroidBase;

        public void Init(DirectionalMoverSettings moverSettings, float damageCount)
        {
            _asteroidBase = GetComponent<AsteroidBase>();
            _asteroidBase.Init(moverSettings, damageCount);
            _asteroidBase.Mover.EnableMoving();
            _asteroidBase.Died += OnDeath;
        }

        public void AddFragment(AsteroidFragment fragment) => Fragments.Add(fragment);

        public void Tick() => _asteroidBase.Tick();

        public void Kill()
        {
            foreach (AsteroidFragment fragment in Fragments)
                fragment.Activate();

            Destroy(gameObject);
        }

        private void OnDeath() => Killed?.Invoke(this);

        private void OnDestroy()
        {
            if (_asteroidBase != null)
                _asteroidBase.Died -= OnDeath;
        }
    }
}
