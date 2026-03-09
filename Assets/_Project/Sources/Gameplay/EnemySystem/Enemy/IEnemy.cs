using Asteroids.GameLoop;
using Asteroids.Gameplay.ObjectMovement;
using System;
using UnityEngine;

namespace Asteroids.Gameplay.EnemySystem
{
    public interface IEnemy : ISceneTickable
    {
        public Action<IEnemy> Killed { get; set; }
        public IMover Mover { get; }
        public GameObject EnemyGameObject { get; }
        public void Kill();
    }
}