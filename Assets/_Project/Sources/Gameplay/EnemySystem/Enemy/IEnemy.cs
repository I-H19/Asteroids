using System;

namespace Asteroids
{
    public interface IEnemy : ISceneTickable
    {
        public IMover Mover { get; }
        public event Action<IEnemy> Killed;
        public void Kill();
    }
}