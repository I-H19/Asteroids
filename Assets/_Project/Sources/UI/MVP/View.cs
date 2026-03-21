using TMPro;
using UnityEngine;

namespace _Project.Sources.UI.MVP
{
    public class View : MonoBehaviour
    {
        private GameObject _restartPanel;
        private TextMeshProUGUI _playerResultScore;
        private TextMeshProUGUI _playerRuntimeScore;
        private TextMeshProUGUI _playerStats;

        public void Init(TextMeshProUGUI playerRuntimeScore, TextMeshProUGUI playerResultScore,
            TextMeshProUGUI playerStats, GameObject restartPanel)
        {
            _playerRuntimeScore = playerRuntimeScore;
            _playerResultScore = playerResultScore;
            _playerStats = playerStats;
            _restartPanel = restartPanel;
        }

        public void ChangeHUDStats(PlayerStats playerStats)
        {
            _playerStats.text =
                $"Coords: {playerStats.PlayerPosition}\n" +
                $"Angle: {playerStats.Angle}\n" +
                $"Speed: {playerStats.Speed}\n" +
                $"LaserAmmo: {playerStats.TotalAmmo}/{playerStats.MaxChargesNumber}\n" +
                $"LaserReloadTime: {playerStats.ReloadingTime}\n" +
                $"LaserCooldown: {playerStats.ShootingCooldown}";
        }

        public void ChangeScore(int score)
        {
            _playerRuntimeScore.text = $"GameScore: {score}";
            _playerResultScore.text = $"GameScore: {score}";
        }

        public void SetRestartPanelEnabled(bool panelEnabled) => _restartPanel.SetActive(panelEnabled);
    }
}