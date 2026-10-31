using VContainer;
using VContainer.Unity;

namespace Asteroids
{
    public class SceneEntryPoint : IInitializable
    {
        private AsteroidFactory _asteroidFactory;
        private BulletSpawner _bulletSpawner;
        private EnemySpawnerSettings _enemySpawnerSettings;

        private GameModel _gameModel;
        private GameplayEventsBridge _gameplayEventsBridge;
        private PlayerStatsBridge _playerStatsBridge;
        private GamePresenter _gamePresenter;
        private Laser _laser;
        private Player _player;
        private PlayerKeyboardController _playerKeyboardController;
        private PlayerMovementSettings _playerMovementSettings;
        private PlayerStats _playerStats;

        private RootUI _rootUI;
        private SpawnOptionsTable _spawnOptionsTable;

        private UfoFactory _ufoFactory;

        public void Initialize()
        {
            InitializePlayer();
            InitializePlayerMovement();
            InitializeUI();
            InitializeGameFlow();

            InitializeSpawnOptions();
        }

        [Inject]
        public void Construct(
            PlayerMovementSettings playerMovementSettings,
            PlayerKeyboardController playerKeyboardController,
            UfoFactory ufoFactory,
            BulletSpawner bulletSpawner,
            Player player,
            RootUI rootUI,
            GamePresenter gamePresenter,
            GameModel gameModel,
            GameplayEventsBridge gameplayEventsBridge,
            PlayerStatsBridge playerStatsBridge,
            PlayerStats playerStats,
            SpawnOptionsTable spawnOptionsTable,
            AsteroidFactory asteroidFactory,
            EnemySpawnerSettings enemySpawnerSettings)
        {
            _player = player;
            _bulletSpawner = bulletSpawner;
            _playerMovementSettings = playerMovementSettings;
            _playerKeyboardController = playerKeyboardController;

            _rootUI = rootUI;
            _gamePresenter = gamePresenter;
            _gameModel = gameModel;
            _gameplayEventsBridge = gameplayEventsBridge;
            _playerStatsBridge = playerStatsBridge;
            _playerStats = playerStats;

            _ufoFactory = ufoFactory;
            _spawnOptionsTable = spawnOptionsTable;
            _asteroidFactory = asteroidFactory;
            _enemySpawnerSettings = enemySpawnerSettings;
        }

        private void InitializeSpawnOptions()
        {
            _spawnOptionsTable.AddSpawnOption(new(_enemySpawnerSettings.AsteroidSpawnChance, _asteroidFactory));

            _spawnOptionsTable.AddSpawnOption(new(_enemySpawnerSettings.UfoSpawnChance, _ufoFactory));
        }

        private void InitializeGameFlow()
        {
            _gameplayEventsBridge.Init();
        }

        private void InitializePlayer()
        {
            _player.Init();
            _laser = _player.PlayerLaser;
            _playerKeyboardController.Init(
                _player.PlayerInertialMover,
                _player.PlayerDirectionalRotator,
                _bulletSpawner,
                _laser);
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

            _gamePresenter.Init(_rootUI.View);

            _playerStatsBridge.Init();
        }
    }
}
