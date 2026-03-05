using System;
using UnityEngine;

public interface IEnemy : ISceneTickable
{
    public Action<IEnemy> Killed { get; set; }
    public IMover Mover { get;  }
    public GameObject EnemyGameObject { get; }
    public void Kill();
}
