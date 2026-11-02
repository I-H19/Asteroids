using System;
using VContainer;

namespace Asteroids
{
    public class PlayerStatsBridge : IDisposable
    {
        private GameModel _gameModel;
        private Player _player;
        private bool _isSubscribed;

        [Inject]
        public void Construct(GameModel gameModel, Player player)
        {
            _gameModel = gameModel;
            _player = player;
        }

        public void Init()
        {
            _gameModel.ChangeMaxCharges(_player.MaxLaserCharges);
            Subscribe();
        }

        public void Dispose()
        {
            if (_isSubscribed)
            {
                Unsubscribe();
            }
        }

        private void Subscribe()
        {
            if (_isSubscribed)
            {
                return;
            }

            _player.SpeedChanged += OnSpeedChanged;
            _player.PositionChanged += OnPositionChanged;
            _player.AngleChanged += OnAngleChanged;
            _player.LaserAmmoChanged += OnLaserAmmoChanged;
            _player.LaserTicked += OnCooldownChanged;
            _isSubscribed = true;
        }

        private void Unsubscribe()
        {
            _player.SpeedChanged -= OnSpeedChanged;
            _player.PositionChanged -= OnPositionChanged;
            _player.AngleChanged -= OnAngleChanged;
            _player.LaserAmmoChanged -= OnLaserAmmoChanged;
            _player.LaserTicked -= OnCooldownChanged;
            _isSubscribed = false;
        }

        private void OnSpeedChanged()
        {
            _gameModel.ChangeSpeed(_player.CurrentSpeed);
        }

        private void OnPositionChanged()
        {
            _gameModel.ChangePosition(_player.Position);
        }

        private void OnAngleChanged()
        {
            _gameModel.ChangeAngle(_player.CurrentAngle);
        }

        private void OnLaserAmmoChanged()
        {
            _gameModel.ChangeTotalAmmo(_player.TotalLaserAmmo);
        }

        private void OnCooldownChanged()
        {
            _gameModel.ChangeCooldown(_player.LaserShootingCooldown, _player.LaserReloadingTime);
        }
    }
}