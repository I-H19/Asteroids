using System;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    [RequireComponent(typeof(AsteroidBase))]
    public class Asteroid : MonoBehaviour, IEnemy
    {
        [field: SerializeField]
        public List<AsteroidFragment> Fragments { get; private set; } = new();

        private AsteroidBase _asteroidBase;

        private void OnDestroy()
        {
            if (_asteroidBase != null)
            {
                _asteroidBase.Died -= OnDied;
            }
        }

        public event Action<IEnemy> Killed;
        public IMover Mover => _asteroidBase.Mover;

        public void FixedTick()
        {
            _asteroidBase.FixedTick();
        }

        public void Kill()
        {
            foreach (AsteroidFragment fragment in Fragments)
            {
                fragment.Activate();
            }

            Destroy(gameObject);
        }

        public void Init(DirectionalMoverSettings moverSettings, float damageCount)
        {
            _asteroidBase = GetComponent<AsteroidBase>();
            _asteroidBase.Init(moverSettings, damageCount);
            _asteroidBase.Mover.EnableMoving();
            _asteroidBase.Died += OnDied;
        }

        public void AddFragment(AsteroidFragment fragment)
        {
            Fragments.Add(fragment);
        }

        private void OnDied()
        {
            Killed?.Invoke(this);
        }
    }
}
