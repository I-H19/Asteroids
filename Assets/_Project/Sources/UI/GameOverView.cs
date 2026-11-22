using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids
{
    public class GameOverView : MonoBehaviour
    {
        [field: SerializeField]
        public TextMeshProUGUI RecordScore { get; private set; }

        [field: SerializeField]
        public TextMeshProUGUI PlayedGamesCount { get; private set; }
        
        [field: SerializeField]
        public Image GameRestartPanel { get; private set; }

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

        public void SetRestartPanelEnabled(bool panelEnabled)
        {
            GameRestartPanel.gameObject.SetActive(panelEnabled);
        }

        private void OnRestartButtonClicked()
        {
            RestartButtonClicked?.Invoke();
        }
        
        public void ChangeRecordScore(int score)
        {
            RecordScore.text = $"Best score: {score}";
        }

        public void ChangePlayedGamesCount(int count)
        {
            PlayedGamesCount.text = $"Games Count: {count}";
        }
    }
}