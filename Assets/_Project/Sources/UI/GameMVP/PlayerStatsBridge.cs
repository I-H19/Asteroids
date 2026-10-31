using System;
using UnityEngine;
using VContainer;

namespace Asteroids
{
    public class PlayerStatsBridge : IDisposable
    {
        private GameModel _gameModel;
        private Laser _laser;
        private Player _player;
        private InertialMover _playerMover;
        private Rigidbody2D _playerRigidBody;
        private DirectionalRotator _playerRotator;
        private bool _isSubscribed;

        public void Dispose()
        {
            if (_isSubscribed)
            {
                Unsubscribe();
            }
        }

        [Inject]
        public void Construct(GameModel gameModel, Player player)
        {
            _gameModel = gameModel;
            _player = player;
        }

        public void Init()
        {
            _playerMover = _player.PlayerInertialMover;
            _playerRotator = _player.PlayerDirectionalRotator;
            _playerRigidBody = _player.PlayerRigidBody;
            _laser = _player.PlayerLaser;

            _gameModel.ChangeMaxCharges(_laser.MaxChargesNumber);
            Subscribe();
        }

        private void Subscribe()
        {
            if (_isSubscribed)
            {
                return;
            }

            _playerMover.SpeedChanged += OnSpeedChanged;
            _playerMover.PositionChanged += OnPositionChanged;
            _playerRotator.AngleChanged += OnAngleChanged;
            _laser.AmmoChanged += OnLaserAmmoChanged;
            _laser.Ticked += OnCooldownChanged;
            _isSubscribed = true;
        }

        private void Unsubscribe()
        {
            _playerMover.SpeedChanged -= OnSpeedChanged;
            _playerMover.PositionChanged -= OnPositionChanged;
            _playerRotator.AngleChanged -= OnAngleChanged;
            _laser.AmmoChanged -= OnLaserAmmoChanged;
            _laser.Ticked -= OnCooldownChanged;
            _isSubscribed = false;
        }

        private void OnSpeedChanged()
        {
            _gameModel.ChangeSpeed(_playerMover.CurrentSpeed);
        }

        private void OnPositionChanged()
        {
            _gameModel.ChangePosition(_playerRigidBody.position);
        }

        private void OnAngleChanged()
        {
            _gameModel.ChangeAngle(_playerRotator.CurrentAngle);
        }

        private void OnLaserAmmoChanged()
        {
            _gameModel.ChangeTotalAmmo(_laser.TotalAmmo);
        }

        private void OnCooldownChanged()
        {
            _gameModel.ChangeCooldown(_laser.ShootingCooldown, _laser.ReloadingTime);
        }
    }
}
