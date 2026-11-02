using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Asteroids
{
    public class SceneScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterEntryPoints(builder);
            RegisterCore(builder);
            RegisterPlayer(builder);
            RegisterGameplay(builder);
            RegisterGameFlow(builder);
            RegisterFactories(builder);

            builder.RegisterComponentInNewPrefab(_prefabHolder.GameViewPrefab, Lifetime.Singleton);
            builder.Register<GamePresenter>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<GameModel>(Lifetime.Singleton);
            builder.Register<GameplayEventsBridge>(Lifetime.Singleton);
            builder.Register<PlayerStatsBridge>(Lifetime.Singleton);
            builder.Register<PlayerStats>(Lifetime.Singleton);
        }

        private static void RegisterEntryPoints(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<SceneEntryPoint>();
        }

        private void RegisterCore(IContainerBuilder builder)
        {
            builder.RegisterComponent(_camera);

            builder.Register<SceneTickDriver>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();

            builder.Register<KeyboardMonitor>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
        }

        private void RegisterPlayer(IContainerBuilder builder)
        {
            builder.RegisterComponentInNewPrefab(_prefabHolder.PlayerRoot, Lifetime.Singleton)
                .AsSelf()
                .AsImplementedInterfaces();


            builder.RegisterComponent(_playerMovementSettings);
            builder.RegisterComponent(_playerCombatSettings);

            builder.RegisterInstance(_playerMovementSettings.MoverSettings);
            builder.RegisterInstance(_playerMovementSettings.RotationSettings);

            builder.Register<BulletRegistry>(Lifetime.Singleton).AsSelf();
            builder.Register<BulletFactory>(Lifetime.Singleton).AsSelf();
            builder.Register<BulletSystem>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<BulletFiringController>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();

            builder.Register<PlayerKeyboardController>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
        }

        private void RegisterGameplay(IContainerBuilder builder)
        {
            builder.RegisterComponent(_prefabHolder);
            builder.RegisterComponent(_enemySpawnerSettings);
            builder.RegisterComponent(_enemySettings);

            builder.Register<ScreenBoundsTracker>(Lifetime.Transient);
            builder.Register<AsteroidSplitter>(Lifetime.Singleton);
            builder.Register<EnemySpawner>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<EnemyDriver>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<EnemyRegistry>(Lifetime.Singleton).AsSelf();
            builder.Register<SpawnOptionsTable>(Lifetime.Singleton);
        }

        private static void RegisterGameFlow(IContainerBuilder builder)
        {
            builder.Register<GameRestarter>(Lifetime.Singleton);
            builder.Register<GamePause>(Lifetime.Singleton);

            builder.Register<GameFinisher>(Lifetime.Singleton).AsSelf();
        }

        private static void RegisterFactories(IContainerBuilder builder)
        {
            builder.Register<AsteroidFactory>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<UfoFactory>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
        }
#pragma warning disable CS0649
        [Header("Core")] [SerializeField] private Camera _camera;

        [Header("Player")] [SerializeField] private PlayerMovementSettings _playerMovementSettings;

        [SerializeField] private PlayerCombatSettings _playerCombatSettings;

        [Header("Gameplay")] [SerializeField] private PrefabHolder _prefabHolder;

        [SerializeField] private EnemySpawnerSettings _enemySpawnerSettings;
        [SerializeField] private EnemySettings _enemySettings;
#pragma warning restore CS0649
    }
}
