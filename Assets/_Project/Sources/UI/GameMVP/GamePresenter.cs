using System;
using VContainer;

namespace _Project.Sources.UI.GameMVP
{
    public class GamePresenter : IDisposable
    {
        private GameModel _gameModel;
        private GameView _gameView;

        [Inject]
        public void Construct(
            GameModel gameModel)
        {
            _gameModel = gameModel;
        }

        public void Init(
            GameView gameView)
        {
            _gameView = gameView;

            Subscribe();
        }

        public void Dispose() => Unsubscribe();

        private void Subscribe()
        {
            _gameModel.OnScoreChanged += OnScoreChanged;
            _gameModel.OnStatsChanged += OnStatsChanged;

            _gameModel.OnGameFinished += ActivateRestartPanel;
            _gameView.OnRestartGameButtonClicked += OnRestartButtonClick;
        }

        private void Unsubscribe()
        {
            _gameModel.OnScoreChanged -= OnScoreChanged;
            _gameModel.OnStatsChanged -= OnStatsChanged;

            _gameModel.OnGameFinished -= ActivateRestartPanel;
            _gameView.OnRestartGameButtonClicked -= OnRestartButtonClick;
        }

        private void OnRestartButtonClick() => DeactivateRestartPanel();

        private void OnScoreChanged() => _gameView.ChangeScore(_gameModel.Score);
        private void OnStatsChanged() => _gameView.ChangeHUDStats(_gameModel.PlayerStats);

        private void ActivateRestartPanel() => _gameView.SetRestartPanelEnabled(true);
        private void DeactivateRestartPanel() => _gameView.SetRestartPanelEnabled(false);
    }
}
