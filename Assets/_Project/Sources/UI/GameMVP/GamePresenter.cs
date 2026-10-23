using System;
using VContainer;

namespace Asteroids
{
    public class GamePresenter : IDisposable
    {
        private GameModel _gameModel;
        private GameView _gameView;

        public void Dispose()
        {
            Unsubscribe();
        }

        [Inject]
        public void Construct(GameModel gameModel)
        {
            _gameModel = gameModel;
        }

        public void Init(GameView gameView)
        {
            _gameView = gameView;

            Subscribe();
        }

        private void Subscribe()
        {
            _gameModel.ScoreChanged += OnScoreChanged;
            _gameModel.StatsChanged += OnStatsChanged;

            _gameModel.GameFinished += OnGameFinished;
            _gameView.RestartButtonClicked += OnRestartButtonClicked;
        }

        private void Unsubscribe()
        {
            _gameModel.ScoreChanged -= OnScoreChanged;
            _gameModel.StatsChanged -= OnStatsChanged;

            _gameModel.GameFinished -= OnGameFinished;
            _gameView.RestartButtonClicked -= OnRestartButtonClicked;
        }

        private void OnRestartButtonClicked()
        {
            DeactivateRestartPanel();
        }

        private void OnScoreChanged()
        {
            _gameView.ChangeScore(_gameModel.Score);
        }

        private void OnStatsChanged()
        {
            _gameView.ChangeHUDStats(_gameModel.PlayerStats);
        }

        private void OnGameFinished()
        {
            _gameView.SetRestartPanelEnabled(true);
        }

        private void DeactivateRestartPanel()
        {
            _gameView.SetRestartPanelEnabled(false);
        }
    }
}