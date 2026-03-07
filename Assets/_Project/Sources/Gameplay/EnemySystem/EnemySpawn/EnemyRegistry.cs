using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class EnemyRegistry : MonoBehaviour
{
    public readonly List<IEnemy> AliveEnemies = new();
    private PlayerScore _playerScore;

    public int AliveCount { get; private set; }

    [Inject]
    public void Construct(PlayerScore playerScore) => _playerScore = playerScore;
    public void RegisterEnemy(IEnemy enemy)
    {
        Component enemyComponent = (Component)enemy;
        GameObject enemyGameObject = enemyComponent.gameObject;

        enemy.Killed += ScoredKill;

        AliveEnemies.Add(enemy);
        AliveCount++;
    }
    public void ScoredKill(IEnemy enemy)
    {
        if (enemy is Asteroid)
        { 
            Asteroid asteroid = (Asteroid)enemy;   
            if (asteroid.IsFragment && !asteroid.IsActiveFragment) return;
            if (!asteroid.IsFragment) asteroid.ActivateFragments();
        }
        AliveCount--;
        AliveEnemies.Remove(enemy);

        enemy.Killed -= ScoredKill;
        enemy.Kill();

        _playerScore.Increment();

    }
    public void KillAll()
    {
        foreach (IEnemy enemy in AliveEnemies)
        {
            if (enemy != null)
            {
                Destroy(enemy.EnemyGameObject);
            }
        }

        AliveEnemies.Clear();
        AliveCount = 0;
    }
    private void OnDestroy() => Unsubscribe();
    private void Unsubscribe()
    {
        foreach (IEnemy enemy in AliveEnemies)
        {
            enemy.Killed -= ScoredKill;
        }
    }
}
