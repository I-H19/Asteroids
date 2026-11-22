using VContainer;
using VContainer.Unity;

namespace Asteroids
{
    public class SceneEntryPoint : IInitializable
    {
        private AsteroidFactory _asteroidFactory;
        private EnemySpawnerSettings _enemySpawnerSettings;

        private GameplayEventsBridge _gameplayEventsBridge;
        private PlayerStatsBridge _playerStatsBridge;
        private Player _player;
        private PlayerKeyboardController _playerKeyboardController;

        private SpawnOptionsTable _spawnOptionsTable;

        private UfoFactory _ufoFactory;

        [Inject]
        public void Construct(
            PlayerKeyboardController playerKeyboardController,
            UfoFactory ufoFactory,
            Player player,
            GameplayEventsBridge gameplayEventsBridge,
            PlayerStatsBridge playerStatsBridge,
            SpawnOptionsTable spawnOptionsTable,
            AsteroidFactory asteroidFactory,
            EnemySpawnerSettings enemySpawnerSettings)
        {
            _player = player;
            _playerKeyboardController = playerKeyboardController;

            _gameplayEventsBridge = gameplayEventsBridge;
            _playerStatsBridge = playerStatsBridge;

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
            _playerKeyboardController.KeyboardSubscribe();
        }

        private void InitializeUI()
        {
            _playerStatsBridge.Init();
        }
    }
}