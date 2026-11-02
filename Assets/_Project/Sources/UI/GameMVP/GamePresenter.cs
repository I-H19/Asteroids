using System;
using VContainer;

namespace Asteroids
{
    public class GamePresenter : IDisposable, ISceneLateTickable
    {
        private GameModel _gameModel;
        private PlayerHudView _playerHudView;
        private ScoreView _scoreView;
        private GameOverView _gameOverView;
        private bool _isScoreDirty = true;
        private bool _isStatsDirty = true;

        [Inject]
        public void Construct(GameModel gameModel, GameView gameView)
        {
            _gameModel = gameModel;
            _playerHudView = gameView.PlayerHudView;
            _scoreView = gameView.ScoreView;
            _gameOverView = gameView.GameOverView;

            Subscribe();
        }

        public void Dispose()
        {
            Unsubscribe();
        }

        public void LateTick()
        {
            if (_isScoreDirty)
            {
                _scoreView.ChangeScore(_gameModel.Score);
                _isScoreDirty = false;
            }

            if (_isStatsDirty)
            {
                _playerHudView.ChangeHUDStats(_gameModel.PlayerStats);
                _isStatsDirty = false;
            }
        }

        private void Subscribe()
        {
            _gameModel.ScoreChanged += OnScoreChanged;
            _gameModel.StatsChanged += OnStatsChanged;

            _gameModel.GameFinished += OnGameFinished;
            _gameOverView.RestartButtonClicked += OnRestartButtonClicked;
        }

        private void Unsubscribe()
        {
            _gameModel.ScoreChanged -= OnScoreChanged;
            _gameModel.StatsChanged -= OnStatsChanged;

            _gameModel.GameFinished -= OnGameFinished;
            _gameOverView.RestartButtonClicked -= OnRestartButtonClicked;
        }

        private void OnRestartButtonClicked()
        {
            DeactivateRestartPanel();
        }

        private void OnScoreChanged()
        {
            _isScoreDirty = true;
        }

        private void OnStatsChanged()
        {
            _isStatsDirty = true;
        }

        private void OnGameFinished()
        {
            _gameOverView.SetRestartPanelEnabled(true);
        }

        private void DeactivateRestartPanel()
        {
            _gameOverView.SetRestartPanelEnabled(false);
        }
    }
}
