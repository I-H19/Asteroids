using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class SpawnOptionsTable
    {
        private readonly List<SpawnOption> _spawnOptions = new();
        private int _totalWeight;

        public IEnemy SpawnRandom(Vector3 spawnPosition)
        {
            if (_totalWeight <= 0)
            {
                return null;
            }

            int randomValue = Random.Range(0, _totalWeight);

            foreach (SpawnOption option in _spawnOptions)
            {
                if (randomValue < option.Weight)
                {
                    return option.Factory.SpawnOne(spawnPosition);
                }

                randomValue -= option.Weight;
            }

            return null;
        }

        public void AddSpawnOption(SpawnOption spawnOption)
        {
            _spawnOptions.Add(spawnOption);
            UpdateTotalWeight();
        }

        private void UpdateTotalWeight()
        {
            _totalWeight = 0;
            foreach (SpawnOption spawnOption in _spawnOptions)
            {
                _totalWeight += spawnOption.Weight;
            }
        }
    }
}