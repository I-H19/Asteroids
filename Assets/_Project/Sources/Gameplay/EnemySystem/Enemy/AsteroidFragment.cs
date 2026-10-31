using System;
using UnityEngine;

namespace Asteroids
{
    [RequireComponent(typeof(AsteroidBase))]
    public class AsteroidFragment : MonoBehaviour, IEnemy
    {
        private AsteroidBase _asteroidBase;

        private Asteroid _parentAsteroid;

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
            Destroy(gameObject);
        }

        public void Init(DirectionalMoverSettings moverSettings, float damageCount)
        {
            _asteroidBase = GetComponent<AsteroidBase>();
            _asteroidBase.Init(moverSettings, damageCount);
            _asteroidBase.Mover.DisableMoving();
            _asteroidBase.Died += OnDied;
        }

        public void Activate()
        {
            gameObject.SetActive(true);
            _asteroidBase.Mover.SetPosition(_parentAsteroid.transform.position);
            _asteroidBase.Mover.EnableMoving();
        }

        public void SetParent(Asteroid parentAsteroid)
        {
            _parentAsteroid = parentAsteroid;
        }

        private void OnDied()
        {
            Killed?.Invoke(this);
        }
    }
}
