using System;
using System.Text;
using _Project.Sources.UI.Elements;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Sources.UI.GameMVP
{
    public class GameView : MonoBehaviour
    {
        private RestartPanel _restartPanel;
        private Button _restartGameButton;
        private TextMeshProUGUI _playerResultScore;
        private TextMeshProUGUI _playerRuntimeScore;
        private TextMeshProUGUI _playerStats;
        
        private readonly StringBuilder _hudBuilder = new();
        
        public event Action OnRestartGameButtonClicked;

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
            _restartGameButton =  restartGameButton;
            
            _restartGameButton.onClick.AddListener(() => OnRestartGameButtonClicked?.Invoke());
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

        public void SetRestartPanelEnabled(bool panelEnabled) => _restartPanel.SetActive(panelEnabled);

        public void OnDestroy()
        {
            _restartGameButton.onClick.RemoveAllListeners();
        }
    }
}
