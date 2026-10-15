using System.Collections;
using System.Collections.Generic;
using _Project.Sources.Gameplay;
using _Project.Sources.Gameplay.EnemySystem.EnemySpawn;
using _Project.Sources.Gameplay.WeaponSystem;
using VContainer;

namespace _Project.Sources.GameLoop
{
    public class GamePause
    {
        private BulletSpawner _bulletSpawner;
        private EnemyDriver _enemyDriver;
        private SceneTickDriver _sceneTickDriver;
        private Player _player;
        private IEnumerable _pausables;

        [Inject]
        public void Construct(IEnumerable<IPausable> pausables) => _pausables = pausables;


        public void SetPause(bool pause)
        {
            if (pause)
                foreach (IPausable pausable in _pausables)
                    pausable.Pause();
            else
                foreach (IPausable pausable in _pausables)
                    pausable.Unpause();
        }
    }
}