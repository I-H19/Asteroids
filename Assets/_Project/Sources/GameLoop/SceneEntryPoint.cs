using _Project.Sources.Config.Movement;
using _Project.Sources.Gameplay;
using _Project.Sources.Gameplay.EnemySystem.EnemySpawn.EnemyFactory;
using _Project.Sources.Gameplay.ObjectMovement.Movers;
using _Project.Sources.Gameplay.ObjectMovement.Rotators;
using _Project.Sources.Gameplay.WeaponSystem;
using _Project.Sources.Input;
using _Project.Sources.UI;
using _Project.Sources.UI.GameMVP;
using VContainer;
using VContainer.Unity;

namespace _Project.Sources.GameLoop
{
    public class SceneEntryPoint : IInitializable
    {
        private Player _player;
        private BulletSpawner _bulletSpawner;
        private Laser _laser;
        private LaserVisual _laserVisual;
        private PlayerMovementSettings _playerMovementSettings;
        private PlayerKeyboardController _playerKeyboardController;

        private GameFinisher _gameFinisher;
        private GameRestarter _gameRestarter;
        private SceneTickDriver _sceneTickDriver;

        private UfoFactory _ufoFactory;

        private RootUI _rootUI;
        private GamePresenter _gamePresenter;
        private GameModel _gameModel;
        private GameplayBridge _gameplayBridge;
        private PlayerStats _playerStats;

        [Inject]
        public void Construct(
            PlayerMovementSettings playerMovementSettings,
            PlayerKeyboardController playerKeyboardController,
            GameFinisher gameFinisher,
            GameRestarter gameRestarter,
            SceneTickDriver sceneTickDriver,
            UfoFactory ufoFactory,
            BulletSpawner bulletSpawner,
            Player player,
            RootUI rootUI,
            GamePresenter gamePresenter,
            GameModel gameModel,
            GameplayBridge gameplayBridge,
            PlayerStats playerStats)
        {
            _player = player;
            _bulletSpawner = bulletSpawner;
            _playerMovementSettings = playerMovementSettings;
            _playerKeyboardController = playerKeyboardController;
 
            _rootUI = rootUI;
            _gamePresenter = gamePresenter;
            _gameModel = gameModel;
            _gameplayBridge = gameplayBridge;
            _playerStats = playerStats;

            _gameFinisher = gameFinisher;
            _gameRestarter = gameRestarter;
            _sceneTickDriver = sceneTickDriver;

            _ufoFactory = ufoFactory;
        }

        public void Initialize()
        {
            InitializePlayer();
            InitializePlayerMovement();
            InitializeUI();
            InitializeGameFlow();
            
            _ufoFactory.Init(_player);
        }

        private void InitializeGameFlow()
        {
            _gameRestarter.Init(_player, _rootUI.RestartButton);
            _gameFinisher.Init(_player);
        }

        private void InitializePlayer()
        {
            _player.Init();
            _laser = _player.PlayerLaser;
            _bulletSpawner.Init(_player);

            _playerKeyboardController.Init(_player.PlayerInertialMover, _player.PlayerDirectionalRotator,
                _bulletSpawner, _laser);
            _playerKeyboardController.KeyboardSubscribe();
        }

        private void InitializePlayerMovement()
        {
            DirectionalRotator directionalRotator = _player.PlayerDirectionalRotator;
            InertialMover inertialMover = _player.PlayerInertialMover;

            directionalRotator.Init(_player.PlayerRigidBody, _playerMovementSettings.RotationSettings);
            inertialMover.Init(_playerMovementSettings.MoverSettings, _player.PlayerRigidBody);
        }

        private void InitializeUI()
        {
            _gameModel.Init(_playerStats);

            _rootUI.View.Init(
                _rootUI.RuntimeScore,
                _rootUI.ResultScore,
                _rootUI.PlayerStats,
                _rootUI.GameRestartPanel,
                _rootUI.RestartButton);

            _gamePresenter.Init(
                _rootUI.View);

            _gameplayBridge.Init();
        }
    }
}