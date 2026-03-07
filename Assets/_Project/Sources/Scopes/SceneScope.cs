using UnityEngine;
using VContainer;
using VContainer.Unity;

public class SceneScope : LifetimeScope
{
    [Header("Core")]
    [SerializeField] private KeyboardMonitor _keyboardMonitor;
    [SerializeField] private Camera _camera;

    [Header("Player")]
    [SerializeField] private Player _player;
    [SerializeField] private PlayerKeyboardController _playerKeyboardController;
    [SerializeField] private PlayerMovementSettings _playerMovementSettings;
    [SerializeField] private PlayerCombatSettings _playerCombatSettings;
    [SerializeField] private PlayerMovementData _playerMovementData;
    [SerializeField] private PlayerScore _playerScore;
    [SerializeField] private BulletSpawner _bulletSpawner;
    [SerializeField] private Laser _playerLaser;

    [Header("Gameplay")]
    [SerializeField] private PrefabHolder _prefabHolder;
    [SerializeField] private SceneObjectHolder _sceneObjectHolder;
    [SerializeField] private ScreenBoundsTrackerSettings _screenBoundsTrackerSettings;
    [SerializeField] private EnemySpawnerSettings _enemySpawnerSettings;
    [SerializeField] private EnemySettings _enemySettings;
    [SerializeField] private EnemyDriver _enemyTickDriver;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private EnemyRegistry _enemyRegistry;

    [Header("UI")]
    [SerializeField] private UIElementsHolder _uiElementsHolder;
    [SerializeField] private UIElementsUpdater _uiElementsUpdater;

    [Header("Game Flow")]
    [SerializeField] private GameRestarter _gameRestarter;
    [SerializeField] private GameFinisher _gameFinisher;
    [SerializeField] private GamePause _gamePause;

    protected override void Configure(IContainerBuilder builder)
    {
        RegisterEntryPoints(builder);
        RegisterCore(builder);
        RegisterPlayer(builder);
        RegisterGameplay(builder);
        RegisterUI(builder);
        RegisterGameFlow(builder);
        RegisterFactories(builder);
    }

    private void RegisterEntryPoints(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<SceneEntryPoint>();
        builder.RegisterEntryPoint<SceneTickDriver>().AsSelf();
    }

    private void RegisterCore(IContainerBuilder builder)
    {
        builder.RegisterComponent(_keyboardMonitor);
        builder.RegisterComponent(_camera);
    }

    private void RegisterPlayer(IContainerBuilder builder)
    {
        builder.RegisterComponent(_player);
        builder.RegisterComponent(_playerKeyboardController);
        builder.RegisterComponent(_playerMovementSettings);
        builder.RegisterComponent(_playerCombatSettings);
        builder.RegisterComponent(_playerMovementData);
        builder.RegisterComponent(_playerScore);
        builder.RegisterComponent(_bulletSpawner);
        builder.RegisterComponent(_playerLaser);

        builder.RegisterInstance(_playerMovementSettings.MoverSettings);
        builder.RegisterInstance(_playerMovementSettings.RotationSettings);
    }

    private void RegisterGameplay(IContainerBuilder builder)
    {
        builder.RegisterComponent(_prefabHolder);
        builder.RegisterComponent(_sceneObjectHolder);
        builder.RegisterComponent(_screenBoundsTrackerSettings);
        builder.RegisterComponent(_enemySpawnerSettings);
        builder.RegisterComponent(_enemySettings);
        builder.RegisterComponent(_enemyTickDriver);
        builder.RegisterComponent(_enemySpawner);
        builder.RegisterComponent(_enemyRegistry);
    }

    private void RegisterUI(IContainerBuilder builder)
    {
        builder.RegisterComponent(_uiElementsHolder);
        builder.RegisterComponent(_uiElementsUpdater);
    }

    private void RegisterGameFlow(IContainerBuilder builder)
    {
        builder.RegisterComponent(_gameRestarter);
        builder.RegisterComponent(_gameFinisher);
        builder.RegisterComponent(_gamePause);
    }

    private static void RegisterFactories(IContainerBuilder builder)
    {
        builder.Register<AsteroidFactory>(Lifetime.Singleton);
        builder.Register<UfoFactory>(Lifetime.Singleton);
    }
}