using _Project.Sources.Config;
using _Project.Sources.Config.Gameplay;
using _Project.Sources.Config.Movement;
using _Project.Sources.GameLoop;
using _Project.Sources.Gameplay.EnemySystem.EnemySpawn;
using _Project.Sources.Gameplay.EnemySystem.EnemySpawn.EnemyFactory;
using _Project.Sources.Gameplay.ObjectMovement;
using _Project.Sources.Gameplay.WeaponSystem;
using _Project.Sources.Input;
using _Project.Sources.UI;
using _Project.Sources.UI.GameMVP;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Sources.Scopes
{
    public class SceneScope : LifetimeScope
    {     
        [Header("Core")] 
        [SerializeField] private Camera _camera;
        
        [Header("Player")] 
        [SerializeField] private PlayerMovementSettings _playerMovementSettings;
        [SerializeField] private PlayerCombatSettings _playerCombatSettings;
 
        [Header("Gameplay")] 
        [SerializeField] private PrefabHolder _prefabHolder;
        [SerializeField] private EnemySpawnerSettings _enemySpawnerSettings;
        [SerializeField] private EnemySettings _enemySettings;

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterEntryPoints(builder);
            RegisterCore(builder);
            RegisterPlayer(builder);
            RegisterGameplay(builder);
            RegisterGameFlow(builder);
            RegisterFactories(builder);
            
            builder.RegisterComponentInNewPrefab(_prefabHolder.UI, Lifetime.Singleton);
            builder.Register<GamePresenter>(Lifetime.Singleton);
            builder.Register<GameModel>(Lifetime.Singleton);
            builder.Register<GameplayBridge>(Lifetime.Singleton);
            builder.Register<PlayerStats>(Lifetime.Singleton);

        }

        private void RegisterEntryPoints(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<SceneEntryPoint>();
        }

        private void RegisterCore(IContainerBuilder builder)
        {
            builder.RegisterComponent(_camera);

            builder.Register<SceneTickDriver>(Lifetime.Singleton)
                .AsSelf()
                .AsImplementedInterfaces();
            
            builder.Register<KeyboardMonitor>(Lifetime.Singleton)
                .AsSelf()
                .AsImplementedInterfaces();;
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
            
            builder.Register<BulletSpawner>(Lifetime.Singleton)
                .AsSelf()
                .AsImplementedInterfaces();
            
            builder.Register<PlayerKeyboardController>(Lifetime.Singleton)
                .AsSelf()
                .AsImplementedInterfaces();
        }

        private void RegisterGameplay(IContainerBuilder builder)
        {
            builder.RegisterComponent(_prefabHolder);
            builder.RegisterComponent(_enemySpawnerSettings);
            builder.RegisterComponent(_enemySettings);

            builder.Register<ScreenBoundsTracker>(Lifetime.Transient);
            builder.Register<AsteroidSplitter>(Lifetime.Singleton);
            builder.Register<EnemySpawner>(Lifetime.Singleton)
                .AsSelf()
                .AsImplementedInterfaces();
            builder.Register<EnemyDriver>(Lifetime.Singleton)
                .AsSelf()
                .AsImplementedInterfaces();
            builder.Register<EnemyRegistry>(Lifetime.Singleton).AsSelf();
            builder.Register<SpawnOptionsTable>(Lifetime.Singleton);
        }
        
        private void RegisterGameFlow(IContainerBuilder builder)
        {
            builder.Register<GameRestarter>(Lifetime.Singleton);
            builder.Register<GamePause>(Lifetime.Singleton);

            builder.Register<GameFinisher>(Lifetime.Singleton).AsSelf();
        }

        private static void RegisterFactories(IContainerBuilder builder)
        {
            builder.Register<AsteroidFactory>(Lifetime.Singleton)
                .AsSelf()
                .AsImplementedInterfaces();
            builder.Register<UfoFactory>(Lifetime.Singleton)
                .AsSelf()
                .AsImplementedInterfaces();
        }
    }
}
