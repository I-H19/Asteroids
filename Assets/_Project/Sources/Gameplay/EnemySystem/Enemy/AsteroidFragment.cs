using System;
using _Project.Sources.Config.Movement;
using _Project.Sources.Gameplay.ObjectMovement.Movers;
using UnityEngine;

namespace _Project.Sources.Gameplay.EnemySystem.Enemy
{
    [RequireComponent(typeof(AsteroidBase))]
    public class AsteroidFragment : MonoBehaviour, IEnemy
    {
        public event Action<IEnemy> Killed;
        public IMover Mover => _asteroidBase.Mover;

        private Asteroid _parentAsteroid;
        private AsteroidBase _asteroidBase;

        public void Init(DirectionalMoverSettings moverSettings, float damageCount)
        {
            _asteroidBase = GetComponent<AsteroidBase>();
            _asteroidBase.Init(moverSettings, damageCount);
            _asteroidBase.Mover.DisableMoving();
            _asteroidBase.Died += OnDeath;
        }

        public void Activate()
        {
            gameObject.SetActive(true);
            _asteroidBase.Mover.SetPosition(_parentAsteroid.transform.position);
            _asteroidBase.Mover.EnableMoving();
        }

        public void SetParent(Asteroid parentAsteroid) => _parentAsteroid = parentAsteroid;

        public void Tick() => _asteroidBase.Tick();

        public void Kill() => Destroy(gameObject);

        private void OnDeath() => Killed?.Invoke(this);

        private void OnDestroy()
        {
            if (_asteroidBase != null)
                _asteroidBase.Died -= OnDeath;
        }
    }
}
