using _Project.Sources.Config;
using _Project.Sources.Config.Gameplay;
using _Project.Sources.Config.Movement;
using _Project.Sources.GameLoop;
using _Project.Sources.Gameplay.EnemySystem.EnemySpawn;
using _Project.Sources.Gameplay.EnemySystem.EnemySpawn.EnemyFactory;
using _Project.Sources.Input;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Sources.Scopes
{
    public class SceneScope : LifetimeScope
    {     
        [Header("Core")] [SerializeField] private Camera _camera;
        [Header("Player")] 
        [SerializeField] private PlayerMovementSettings _playerMovementSettings;
        [SerializeField] private PlayerCombatSettings _playerCombatSettings;
 
        [Header("Gameplay")] 
        [SerializeField] private PrefabHolder _prefabHolder;
        [SerializeField] private EnemySpawnerSettings _enemySpawnerSettings;
        [SerializeField] private EnemySettings _enemySettings;
        [SerializeField] private EnemySpawner _enemySpawner;

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterEntryPoints(builder);
            RegisterCore(builder);
            RegisterPlayer(builder);
            RegisterGameplay(builder);
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
            builder.RegisterComponent(_playerMovementSettings);
            builder.RegisterComponent(_playerCombatSettings);
            
            builder.RegisterInstance(_playerMovementSettings.MoverSettings);
            builder.RegisterInstance(_playerMovementSettings.RotationSettings);
            
            builder.Register<PlayerKeyboardController>(Lifetime.Scoped).AsSelf();
        }

        private void RegisterGameplay(IContainerBuilder builder)
        {
            builder.RegisterComponent(_prefabHolder);
            builder.RegisterComponent(_enemySpawnerSettings);
            builder.RegisterComponent(_enemySettings);
            builder.RegisterComponent(_enemySpawner);

            builder.Register<EnemyDriver>(Lifetime.Singleton);

            builder.Register<EnemyRegistry>(Lifetime.Scoped).AsSelf();
        }
        
        private void RegisterGameFlow(IContainerBuilder builder)
        {
            builder.Register<GameRestarter>(Lifetime.Singleton);
            builder.Register<GamePause>(Lifetime.Singleton);

            builder.Register<GameFinisher>(Lifetime.Scoped).AsSelf();
        }

        private static void RegisterFactories(IContainerBuilder builder)
        {
            builder.Register<AsteroidFactory>(Lifetime.Singleton);
            builder.Register<UfoFactory>(Lifetime.Singleton);
        }
    }
}