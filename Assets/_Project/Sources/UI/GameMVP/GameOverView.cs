using System;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids
{
    public class GameOverView : MonoBehaviour
    {
        [field: SerializeField]
        public RestartPanel GameRestartPanel { get; private set; }

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
            GameRestartPanel.SetActive(panelEnabled);
        }

        private void OnRestartButtonClicked()
        {
            RestartButtonClicked?.Invoke();
        }
    }
}