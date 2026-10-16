using System.Collections.Generic;
using _Project.Sources.Gameplay.EnemySystem.Enemy;
using UnityEngine;

public class SpawnOptionsTable
{
    private readonly List<SpawnOption> _spawnOptions = new();
    private int _totalRate;
    
    public IEnemy SpawnOneByChance(Vector3 spawnPosition)
    {
        if (_totalRate <= 0)
            return null;

        int randomValue = Random.Range(0, _totalRate);

        foreach (var option in _spawnOptions)
        {
            if (randomValue < option.Rate)
                return option.Factory.SpawnOne(spawnPosition);

            randomValue -= option.Rate;
        }

        return null;
    }
    
    public void AddSpawnOption(SpawnOption spawnOption)
    {
        _spawnOptions.Add(spawnOption);
        UpdateTotalRate();
    }
    
    private void UpdateTotalRate()
    {
        _totalRate = 0;
        foreach (var spawnOption in _spawnOptions)
            _totalRate += spawnOption.Rate;
    }
}
