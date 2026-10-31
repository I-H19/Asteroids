using System;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids
{
    public class GameView : MonoBehaviour
    {
        private readonly StringBuilder _hudBuilder = new();
        private TextMeshProUGUI _playerResultScore;
        private TextMeshProUGUI _playerRuntimeScore;
        private TextMeshProUGUI _playerStats;
        private Button _restartGameButton;
        private RestartPanel _restartPanel;

        public void OnDestroy()
        {
            _restartGameButton.onClick.RemoveAllListeners();
        }

        public event Action RestartButtonClicked;

        public void Init(
            TextMeshProUGUI playerRuntimeScore,
            TextMeshProUGUI playerResultScore,
            TextMeshProUGUI playerStats,
            RestartPanel restartPanel,
            Button restartGameButton)
        {
            _playerRuntimeScore = playerRuntimeScore;
            _playerResultScore = playerResultScore;
            _playerStats = playerStats;
            _restartPanel = restartPanel;
            _restartGameButton = restartGameButton;

            _restartGameButton.onClick.AddListener(OnRestartButtonClicked);
        }

        private void OnRestartButtonClicked()
        {
            RestartButtonClicked?.Invoke();
        }


        public void ChangeHUDStats(PlayerStats playerStats)
        {
            _hudBuilder.Clear();

            _hudBuilder.AppendLine($"Coords: {playerStats.PlayerPosition}");
            _hudBuilder.AppendLine($"Angle: {playerStats.Angle}");
            _hudBuilder.AppendLine($"Speed: {playerStats.Speed}");
            _hudBuilder.AppendLine($"LaserAmmo: {playerStats.TotalAmmo}/{playerStats.MaxChargesNumber}");
            _hudBuilder.AppendLine($"LaserReloadTime: {playerStats.ReloadingTime}");
            _hudBuilder.AppendLine($"LaserCooldown: {playerStats.ShootingCooldown}");

            _playerStats.SetText(_hudBuilder.ToString());
        }

        public void ChangeScore(int score)
        {
            _playerRuntimeScore.text = $"GameScore: {score}";
            _playerResultScore.text = $"GameScore: {score}";
        }

        public void SetRestartPanelEnabled(bool panelEnabled)
        {
            _restartPanel.SetActive(panelEnabled);
        }
    }
}