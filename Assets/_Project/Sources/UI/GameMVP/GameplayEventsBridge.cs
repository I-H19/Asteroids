using System;
using VContainer;

namespace Asteroids
{
    public class GameplayEventsBridge : IDisposable
    {
        private EnemyRegistry _enemyRegistry;
        private GameFinisher _gameFinisher;
        private GameModel _gameModel;
        private GameRestarter _gameRestarter;
        private bool _isSubscribed;

        [Inject]
        public void Construct(
            EnemyRegistry enemyRegistry,
            GameRestarter gameRestarter,
            GameFinisher gameFinisher,
            GameModel gameModel)
        {
            _enemyRegistry = enemyRegistry;
            _gameRestarter = gameRestarter;
            _gameFinisher = gameFinisher;
            _gameModel = gameModel;
        }

        public void Init()
        {
            Subscribe();
        }

        public void Dispose()
        {
            if (_isSubscribed)
            {
                Unsubscribe();
            }
        }

        private void Subscribe()
        {
            if (_isSubscribed)
            {
                return;
            }

            _enemyRegistry.EnemyKilled += OnEnemyKilled;
            _gameRestarter.Restarted += OnGameRestarted;
            _gameFinisher.Finished += OnGameFinished;
            _isSubscribed = true;
        }

        private void Unsubscribe()
        {
            _enemyRegistry.EnemyKilled -= OnEnemyKilled;
            _gameRestarter.Restarted -= OnGameRestarted;
            _gameFinisher.Finished -= OnGameFinished;
            _isSubscribed = false;
        }

        private void OnEnemyKilled()
        {
            _gameModel.IncrementScore();
        }

        private void OnGameRestarted()
        {
            _gameModel.ResetScore();
        }

        private void OnGameFinished()
        {
            _gameModel.FinishGame();
        }
    }
}