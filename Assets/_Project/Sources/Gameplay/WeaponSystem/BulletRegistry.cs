using System;
using System.Collections.Generic;

namespace Asteroids
{
    public class BulletRegistry
    {
        private readonly List<Bullet> _activeBullets = new();

        public void Register(Bullet bullet)
        {
            if (bullet == null || _activeBullets.Contains(bullet))
            {
                return;
            }

            _activeBullets.Add(bullet);
            bullet.Destroyed += OnBulletDestroyed;
        }

        public void ForEach(Action<Bullet> action)
        {
            for (int i = 0; i < _activeBullets.Count; i++)
            {
                action(_activeBullets[i]);
            }
        }

        public void ClearAll()
        {
            Bullet[] bullets = _activeBullets.ToArray();
            _activeBullets.Clear();

            foreach (Bullet bullet in bullets)
            {
                bullet.Destroyed -= OnBulletDestroyed;
                bullet.DestroyBullet();
            }
        }

        private void OnBulletDestroyed(Bullet bullet)
        {
            bullet.Destroyed -= OnBulletDestroyed;
            _activeBullets.Remove(bullet);
        }
    }
}