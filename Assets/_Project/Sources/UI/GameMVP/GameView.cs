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

        [field: SerializeField]
        public RestartPanel GameRestartPanel { get; private set; }

        [field: SerializeField]
        public TextMeshProUGUI RuntimeScore { get; private set; }

        [field: SerializeField]
        public TextMeshProUGUI ResultScore { get; private set; }

        [field: SerializeField]
        public TextMeshProUGUI PlayerStats { get; private set; }

        [field: SerializeField]
        public Button RestartButton { get; private set; }

        public event Action RestartButtonClicked;

        private void OnEnable()
        {
            RestartButton.onClick.AddListener(OnRestartButtonClicked);
        }

        private void OnDisable()
        {
            RestartButton.onClick.RemoveListener(OnRestartButtonClicked);
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

            PlayerStats.SetText(_hudBuilder.ToString());
        }

        public void ChangeScore(int score)
        {
            RuntimeScore.text = $"GameScore: {score}";
            ResultScore.text = $"GameScore: {score}";
        }

        public void SetRestartPanelEnabled(bool panelEnabled)
        {
            GameRestartPanel.SetActive(panelEnabled);
        }
    }
}
