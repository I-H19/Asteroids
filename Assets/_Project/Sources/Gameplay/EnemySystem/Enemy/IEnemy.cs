using System;
using _Project.Sources.GameLoop;
using _Project.Sources.Gameplay.ObjectMovement.Movers;
using UnityEngine;

namespace _Project.Sources.Gameplay.EnemySystem.Enemy
{
    public interface IEnemy : ISceneTickable
    {
        public Action<IEnemy> Killed { get; set; }
        public IMover Mover { get; }
        public void Kill();
    }
}