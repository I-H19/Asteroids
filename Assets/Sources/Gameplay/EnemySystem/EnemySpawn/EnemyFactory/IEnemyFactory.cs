using UnityEngine;

public interface IEnemyFactory
{
    public IEnemy SpawnOne(Vector3 position);
}
