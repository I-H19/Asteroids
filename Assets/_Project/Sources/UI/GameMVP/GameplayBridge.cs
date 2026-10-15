using System;
using _Project.Sources.GameLoop;
using _Project.Sources.Gameplay;
using _Project.Sources.Gameplay.EnemySystem.EnemySpawn;
using _Project.Sources.Gameplay.ObjectMovement.Movers;
using _Project.Sources.Gameplay.ObjectMovement.Rotators;
using _Project.Sources.Gameplay.WeaponSystem;
using UnityEngine;
using VContainer;

namespace _Project.Sources.UI.GameMVP
{
    public class GameplayBridge : IDisposable
    {
        private GameModel _gameModel;

        private Player _player;
        private Rigidbody2D _playerRigidBody;
        private Laser _laser;
        private InertialMover _playerMover;
        private DirectionalRotator _playerRotator;

        private EnemyRegistry _enemyRegistry;

        private GameRestarter _gameRestarter;
        private GameFinisher _gameFinisher;

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
        
        public void Dispose() => Unsubscribe();

        private void Subscribe()
        {
            _enemyRegistry.ScoredKilled += OnScoreIncrement;
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
            _enemyRegistry.ScoredKilled -= OnScoreIncrement;
            _gameRestarter.Restarted -= OnGameRestarted;

            _playerMover.SpeedChanged -= OnSpeedChanged;
            _playerMover.PositionChanged -= OnPositionChanged;
            _playerRotator.AngleChanged -= OnAngleChanged;

            _laser.AmmoChanged -= OnLaserAmmoChanged;
            _laser.Ticked -= OnCooldownChanged;

            _gameFinisher.Finished -= OnGameFinished;
        }

        private void OnScoreIncrement() => _gameModel.ScoreIncrement();
        private void OnGameRestarted() => _gameModel.ResetScore();
        private void OnGameFinished() => _gameModel.GameFinish();

        private void OnSpeedChanged() => _gameModel.ChangeSpeed(_playerMover.CurrentSpeed);
        private void OnPositionChanged() => _gameModel.ChangePosition(_playerRigidBody.position);
        private void OnAngleChanged() => _gameModel.ChangeAngle(_playerRotator.CurrentAngle);

        private void OnLaserAmmoChanged() => _gameModel.ChangeTotalAmmo(_laser.TotalAmmo);

        private void OnCooldownChanged() =>
            _gameModel.ChangeCooldown(_laser.ShootingCooldown, _laser.ReloadingTime);
    }
}
