using System;
using _Project.Sources.GameLoop;
using _Project.Sources.Gameplay;
using _Project.Sources.Gameplay.EnemySystem.EnemySpawn;
using _Project.Sources.Gameplay.ObjectMovement.Movers;
using _Project.Sources.Gameplay.ObjectMovement.Rotators;
using _Project.Sources.Gameplay.WeaponSystem;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Project.Sources.UI.GameMVP
{
    public class Presenter : IDisposable
    {
        private Model _model;
        private View _view;
        private Button _restartGameButton;

        private Player _player;
        private Rigidbody2D _playerRigidBody;
        private Laser _laser;
        private InertialMover _playerMover;
        private DirectionalRotator _playerRotator;

        private EnemyRegistry _enemyRegistry;

        private GameRestarter _gameRestarter;
        private GameFinisher _gameFinisher;

        [Inject]
        public void Construct(EnemyRegistry enemyRegistry, GameRestarter gameRestarter, GameFinisher gameFinisher)
        {
            _enemyRegistry = enemyRegistry;

            _gameRestarter = gameRestarter;
            _gameFinisher = gameFinisher;
        }

        public void Init(Model model, View view, Player player, Button restartGameButton)
        {
            _model = model;
            _view = view;

            _restartGameButton = restartGameButton;
            
            _player = player;
            _playerMover = _player.InertialMoverTemplate;
            _playerRotator = _player.DirectionalRotatorTemplate;

            _playerRigidBody = _player.RigidBodyTemplate;

            _laser = _player.PlayerLaser;

            ChangeMaxChargesNumber(_laser.MaxChargesNumber);
            Subscribe();
        }

        public void Dispose() => Unsubscribe();

        private void Subscribe()
        {
            _enemyRegistry.ScoredKilled += OnScoreIncrement;
            _gameRestarter.Restarted += ResetScore;

            _playerMover.SpeedChanged += OnSpeedChanged;
            _playerRotator.AngleChanged += OnAngleChanged;

            _playerMover.PositionChanged += OnPositionChanged;

            _laser.AmmoChanged += OnLaserAmmoChanged;

            _laser.Ticked += OnCooldownChanged;

            _restartGameButton.onClick.AddListener(OnRestartButtonClick);
            _gameFinisher.Finished += ActivateRestartPanel;
        }

        private void Unsubscribe()
        {
            _enemyRegistry.ScoredKilled -= OnScoreIncrement;
            _gameRestarter.Restarted -= ResetScore;

            _playerMover.SpeedChanged -= OnSpeedChanged;
            _playerRotator.AngleChanged -= OnAngleChanged;

            _playerMover.PositionChanged -= OnPositionChanged;
            _laser.AmmoChanged -= OnLaserAmmoChanged;

            _laser.Ticked -= OnCooldownChanged;
            _gameFinisher.Finished -= ActivateRestartPanel;
            _restartGameButton.onClick.RemoveAllListeners();            
        }

        private void OnRestartButtonClick()
        {
            _gameRestarter.RestartGame();
            DeactivateRestartPanel();
        } 
        
        private void OnCooldownChanged()
        {
            _model.PlayerStats.ChangeShootingCooldown(_laser.ShootingCooldown);
            _model.PlayerStats.ChangeReloadingTime(_laser.ReloadingTime);

            _view.ChangeHUDStats(_model.PlayerStats);
        }

        private void OnLaserAmmoChanged()
        {
            _model.PlayerStats.ChangeTotalAmmo(_laser.TotalAmmo);
            _view.ChangeHUDStats(_model.PlayerStats);
        }

        private void OnPositionChanged()
        {
            _model.PlayerStats.ChangePosition(_playerRigidBody.position);
            _view.ChangeHUDStats(_model.PlayerStats);
        }

        private void OnAngleChanged()
        {
            _model.PlayerStats.ChangeAngle(_playerRotator.CurrentAngle);
            _view.ChangeHUDStats(_model.PlayerStats);
        }

        private void OnSpeedChanged()
        {
            _model.PlayerStats.ChangeSpeed(_playerMover.CurrentSpeed);
            _view.ChangeHUDStats(_model.PlayerStats);
        }

        private void OnScoreIncrement()
        {
            _model.ScoreIncrement();
            _view.ChangeScore(_model.Score);
        }

        private void ActivateRestartPanel() => _view.SetRestartPanelEnabled(true);
        private void DeactivateRestartPanel() => _view.SetRestartPanelEnabled(false);
        
        private void ResetScore()
        {
            _model.ResetScore();
            _view.ChangeScore(_model.Score);
        }

        private void ChangeMaxChargesNumber(int maxChargesNumber)
        {
            _model.PlayerStats.ChangeMaxCharges(maxChargesNumber);
            _view.ChangeHUDStats(_model.PlayerStats);
        }
    }
}