using System;
using UnityEngine;
using VContainer;

namespace Asteroids
{
    public class GameplayBridge : IDisposable
    {
        private EnemyRegistry _enemyRegistry;
        private GameFinisher _gameFinisher;
        private GameModel _gameModel;

        private GameRestarter _gameRestarter;
        private Laser _laser;

        private Player _player;
        private InertialMover _playerMover;
        private Rigidbody2D _playerRigidBody;
        private DirectionalRotator _playerRotator;

        public void Dispose()
        {
            Unsubscribe();
        }

        [Inject]
        public void Construct(
            EnemyRegistry enemyRegistry,
            GameRestarter gameRestarter,
            GameFinisher gameFinisher,
            GameModel gameModel,
            Player player)
        {
            _player = player;
            _enemyRegistry = enemyRegistry;

            _gameRestarter = gameRestarter;
            _gameFinisher = gameFinisher;

            _gameModel = gameModel;
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
            _enemyRegistry.EnemyKilled += OnEnemyKilled;
            _gameRestarter.Restarted += OnGameRestarted;

            _playerMover.SpeedChanged += OnSpeedChanged;
            _playerMover.PositionChanged += OnPositionChanged;
            _playerRotator.AngleChanged += OnAngleChanged;

            _laser.AmmoChanged += OnLaserAmmoChanged;
            _laser.Ticked += OnCooldownChanged;

            _gameFinisher.Finished += OnGameFinished;
        }

        private void Unsubscribe()
        {
            _enemyRegistry.EnemyKilled -= OnEnemyKilled;
            _gameRestarter.Restarted -= OnGameRestarted;

            _playerMover.SpeedChanged -= OnSpeedChanged;
            _playerMover.PositionChanged -= OnPositionChanged;
            _playerRotator.AngleChanged -= OnAngleChanged;

            _laser.AmmoChanged -= OnLaserAmmoChanged;
            _laser.Ticked -= OnCooldownChanged;

            _gameFinisher.Finished -= OnGameFinished;
        }

        private void OnEnemyKilled()
        {
            _gameModel.IncrementScore();
        }

        private void OnGameRestarted()
        {
            _gameModel.ResetScore();
        }

        private void OnGameFinished()
        {
            _gameModel.FinishGame();
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
