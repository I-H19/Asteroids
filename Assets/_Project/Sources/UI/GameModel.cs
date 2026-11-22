using System;
using UnityEngine;
using VContainer;

namespace Asteroids
{
    public class GameModel
    {
        private ISaveLoadProgress _saveLoadProgress;
        private SaveData _actualSaveData;
        public int Score { get; private set; }
        public int BestScore { get; private set; }
        public int PlayedGamesCount { get; private set; }
        public PlayerStats PlayerStats { get; private set; }
        public event Action ScoreChanged;
        public event Action StatsChanged;
        public event Action GameFinished;
        public event Action RecordScoreChanged;
        public event Action PlayedGamesCountChanged;

        [Inject]
        public void Construct(PlayerStats playerStats, ISaveLoadProgress saveLoadProgress)
        {
            PlayerStats = playerStats;
            _saveLoadProgress = saveLoadProgress;

            _actualSaveData = _saveLoadProgress.LoadProgress();
            ChangeRecordScore(_actualSaveData.BestScore);
            ChangePlayedGamesCount(_actualSaveData.PlayedGamesCount);
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

        public void ChangeRecordScore(int score)
        {
            BestScore = score;
            _actualSaveData.BestScore = score;
            RecordScoreChanged?.Invoke();
        }

        public void ChangePlayedGamesCount(int count)
        {
            PlayedGamesCount = count;
            _actualSaveData.PlayedGamesCount = count;
            PlayedGamesCountChanged?.Invoke();
        }

        public void FinishGame()
        {
            if (Score > BestScore)
            {
                BestScore = Score;
                _actualSaveData.BestScore = BestScore;
            }

            ChangePlayedGamesCount(PlayedGamesCount + 1);

            _saveLoadProgress.SaveProgress(_actualSaveData);

            GameFinished?.Invoke();
        }
    }
}