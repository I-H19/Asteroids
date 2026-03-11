using _Project.Sources.GameLoop;
using _Project.Sources.Gameplay;
using _Project.Sources.Gameplay.EnemySystem.EnemySpawn;
using _Project.Sources.Gameplay.EnemySystem.EnemySpawn.EnemyFactory;
using _Project.Sources.Gameplay.WeaponSystem;
using _Project.Sources.Input;
using _Project.Sources.Settings;
using _Project.Sources.Settings.Gameplay;
using _Project.Sources.Settings.Movement;
using _Project.Sources.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Sources.Scopes
{
    public class SceneScope : LifetimeScope
    {
        [Header("Core")] [SerializeField] private Camera _camera;

        [Header("Player")] [SerializeField] private Player _player;
        [SerializeField] private PlayerMovementSettings _playerMovementSettings;
        [SerializeField] private PlayerCombatSettings _playerCombatSettings;
        [SerializeField] private Laser _playerLaser;

        [Header("Gameplay")] [SerializeField] private PrefabHolder _prefabHolder;
        [SerializeField] private SceneObjectHolder _sceneObjectHolder;
        [SerializeField] private ScreenBoundsTrackerSettings _screenBoundsTrackerSettings;
        [SerializeField] private EnemySpawnerSettings _enemySpawnerSettings;
        [SerializeField] private EnemySettings _enemySettings;
        [SerializeField] private EnemySpawner _enemySpawner;

        [Header("UI")] [SerializeField] private UIElementsHolder _uiElementsHolder;

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
            builder.RegisterComponent(_camera);

            builder.Register<KeyboardMonitor>(Lifetime.Singleton);
        }

        private void RegisterPlayer(IContainerBuilder builder)
        {
            builder.RegisterComponent(_player);
            builder.RegisterComponent(_playerMovementSettings);
            builder.RegisterComponent(_playerCombatSettings);
            builder.RegisterComponent(_playerLaser);

            builder.Register<BulletSpawner>(Lifetime.Singleton);
            builder.Register<PlayerScore>(Lifetime.Singleton);

            builder.RegisterInstance(_playerMovementSettings.MoverSettings);
            builder.RegisterInstance(_playerMovementSettings.RotationSettings);

            builder.Register<PlayerMovementData>(Lifetime.Scoped).AsSelf().AsImplementedInterfaces();

            builder.Register<PlayerKeyboardController>(Lifetime.Scoped).AsSelf().AsImplementedInterfaces();
        }

        private void RegisterGameplay(IContainerBuilder builder)
        {
            builder.RegisterComponent(_prefabHolder);
            builder.RegisterComponent(_sceneObjectHolder);
            builder.RegisterComponent(_screenBoundsTrackerSettings);
            builder.RegisterComponent(_enemySpawnerSettings);
            builder.RegisterComponent(_enemySettings);
            builder.RegisterComponent(_enemySpawner);

            builder.Register<EnemyDriver>(Lifetime.Singleton);

            builder.Register<EnemyRegistry>(Lifetime.Scoped).AsSelf().AsImplementedInterfaces();
        }

        private void RegisterUI(IContainerBuilder builder)
        {
            builder.RegisterComponent(_uiElementsHolder);

            builder.Register<UIElementsUpdater>(Lifetime.Scoped).AsSelf().AsImplementedInterfaces();
        }

        private void RegisterGameFlow(IContainerBuilder builder)
        {
            builder.Register<GameRestarter>(Lifetime.Singleton);
            builder.Register<GamePause>(Lifetime.Singleton);

            builder.Register<GameFinisher>(Lifetime.Scoped).AsSelf().AsImplementedInterfaces();
        }

        private static void RegisterFactories(IContainerBuilder builder)
        {
            builder.Register<AsteroidFactory>(Lifetime.Singleton);
            builder.Register<UfoFactory>(Lifetime.Singleton);
        }
    }
}