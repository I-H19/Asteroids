using System;
using _Project.Sources.GameLoop;
using _Project.Sources.Gameplay.ObjectMovement.Movers;

namespace _Project.Sources.Gameplay.EnemySystem.Enemy
{
    public interface IEnemy : ISceneTickable
    {
        public event Action<IEnemy> Killed;
        public IMover Mover { get; }
        public void Kill();
    }
}