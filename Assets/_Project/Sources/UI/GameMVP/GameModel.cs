using System;
using UnityEngine;

namespace Asteroids
{
    public class GameModel
    {
        public int Score { get; private set; }
        public PlayerStats PlayerStats { get; private set; }
        public event Action ScoreChanged;
        public event Action StatsChanged;
        public event Action GameFinished;

        public void Init(PlayerStats playerStats)
        {
            PlayerStats = playerStats;
        }

        public void IncrementScore()
        {
            Score++;
            ScoreChanged?.Invoke();
        }

        public void ResetScore()
        {
            Score = 0;
            ScoreChanged?.Invoke();
        }

        public void ChangePosition(Vector2 position)
        {
            PlayerStats.ChangePosition(position);
            StatsChanged?.Invoke();
        }

        public void ChangeAngle(float angle)
        {
            PlayerStats.ChangeAngle(angle);
            StatsChanged?.Invoke();
        }

        public void ChangeSpeed(float speed)
        {
            PlayerStats.ChangeSpeed(speed);
            StatsChanged?.Invoke();
        }

        public void ChangeTotalAmmo(int totalAmmo)
        {
            PlayerStats.ChangeTotalAmmo(totalAmmo);
            StatsChanged?.Invoke();
        }

        public void ChangeMaxCharges(int maxCharges)
        {
            PlayerStats.ChangeMaxCharges(maxCharges);
            StatsChanged?.Invoke();
        }

        public void ChangeCooldown(float shootingCooldown, float reloadingTime)
        {
            PlayerStats.ChangeShootingCooldown(shootingCooldown);
            PlayerStats.ChangeReloadingTime(reloadingTime);
            StatsChanged?.Invoke();
        }

        public void FinishGame()
        {
            GameFinished?.Invoke();
        }
    }
}
