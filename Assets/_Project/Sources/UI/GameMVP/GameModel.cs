using System;
using UnityEngine;

namespace _Project.Sources.UI.GameMVP
{
    public class GameModel
    {
        public event Action OnScoreChanged;
        public event Action OnStatsChanged;
        public event Action OnGameFinished;
        public int Score { get; private set; } = 0;
        public PlayerStats PlayerStats { get; private set; }

        public void Init(PlayerStats playerStats)
        {
            PlayerStats = playerStats;
        }

        public void ScoreIncrement()
        {
            Score++;
            OnScoreChanged?.Invoke();
        }

        public void ResetScore()
        {
            Score = 0;
            OnScoreChanged?.Invoke();
        }

        public void ChangePosition(Vector2 position)
        {
            PlayerStats.ChangePosition(position);
            OnStatsChanged?.Invoke();
        }

        public void ChangeAngle(float angle)
        {
            PlayerStats.ChangeAngle(angle);
            OnStatsChanged?.Invoke();
        }

        public void ChangeSpeed(float speed)
        {
            PlayerStats.ChangeSpeed(speed);
            OnStatsChanged?.Invoke();
        }

        public void ChangeTotalAmmo(int totalAmmo)
        {
            PlayerStats.ChangeTotalAmmo(totalAmmo);
            OnStatsChanged?.Invoke();
        }

        public void ChangeMaxCharges(int maxCharges)
        {
            PlayerStats.ChangeMaxCharges(maxCharges);
            OnStatsChanged?.Invoke();
        }

        public void ChangeCooldown(float shootingCooldown, float reloadingTime)
        {
            PlayerStats.ChangeShootingCooldown(shootingCooldown);
            PlayerStats.ChangeReloadingTime(reloadingTime);
            OnStatsChanged?.Invoke();
        }
        public void GameFinish()
        {
            OnGameFinished?.Invoke();
        }
    }
}
