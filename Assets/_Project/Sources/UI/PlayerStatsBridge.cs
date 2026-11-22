using System;
using VContainer;

namespace Asteroids
{
    public class PlayerStatsBridge : IDisposable
    {
        private GameModel _gameModel;
        private Player _player;

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
            Unsubscribe();
        }

        private void Subscribe()
        {
            _player.SpeedChanged += OnSpeedChanged;
            _player.PositionChanged += OnPositionChanged;
            _player.AngleChanged += OnAngleChanged;
            _player.LaserAmmoChanged += OnLaserAmmoChanged;
            _player.LaserCooldownChanged += OnLaserCooldownChanged;
        }

        private void Unsubscribe()
        {
            _player.SpeedChanged -= OnSpeedChanged;
            _player.PositionChanged -= OnPositionChanged;
            _player.AngleChanged -= OnAngleChanged;
            _player.LaserAmmoChanged -= OnLaserAmmoChanged;
            _player.LaserCooldownChanged -= OnLaserCooldownChanged;
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

        private void OnLaserCooldownChanged()
        {
            _gameModel.ChangeCooldown(_player.LaserShootingCooldown, _player.LaserReloadingTime);
        }
    }
}