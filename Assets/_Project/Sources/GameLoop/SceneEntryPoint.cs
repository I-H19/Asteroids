using VContainer;
using VContainer.Unity;

namespace Asteroids
{
    public class SceneEntryPoint : IInitializable
    {
        private AsteroidFactory _asteroidFactory;
        private BulletFiringController _bulletFiringController;
        private EnemySpawnerSettings _enemySpawnerSettings;

        private GameModel _gameModel;
        private GameplayEventsBridge _gameplayEventsBridge;
        private PlayerStatsBridge _playerStatsBridge;
        private Player _player;
        private PlayerKeyboardController _playerKeyboardController;
        private PlayerStats _playerStats;

        private SpawnOptionsTable _spawnOptionsTable;

        private UfoFactory _ufoFactory;

        [Inject]
        public void Construct(
            PlayerKeyboardController playerKeyboardController,
            UfoFactory ufoFactory,
            BulletFiringController bulletFiringController,
            Player player,
            GameModel gameModel,
            GameplayEventsBridge gameplayEventsBridge,
            PlayerStatsBridge playerStatsBridge,
            PlayerStats playerStats,
            SpawnOptionsTable spawnOptionsTable,
            AsteroidFactory asteroidFactory,
            EnemySpawnerSettings enemySpawnerSettings)
        {
            _player = player;
            _bulletFiringController = bulletFiringController;
            _playerKeyboardController = playerKeyboardController;

            _gameModel = gameModel;
            _gameplayEventsBridge = gameplayEventsBridge;
            _playerStatsBridge = playerStatsBridge;
            _playerStats = playerStats;

            _ufoFactory = ufoFactory;
            _spawnOptionsTable = spawnOptionsTable;
            _asteroidFactory = asteroidFactory;
            _enemySpawnerSettings = enemySpawnerSettings;
        }

        public void Initialize()
        {
            InitializePlayer();
            InitializeUI();
            InitializeGameFlow();

            InitializeSpawnOptions();
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
            _playerKeyboardController.Init(_player, _bulletFiringController);
            _playerKeyboardController.KeyboardSubscribe();
        }

        private void InitializeUI()
        {
            _gameModel.Init(_playerStats);

            _playerStatsBridge.Init();
        }
    }
}