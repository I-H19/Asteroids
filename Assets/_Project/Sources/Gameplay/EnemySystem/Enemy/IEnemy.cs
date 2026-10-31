using System;

namespace Asteroids
{
    public interface IEnemy : ISceneFixedTickable
    {
        public IMover Mover { get; }
        public event Action<IEnemy> Killed;
        public void Kill();
    }
}
