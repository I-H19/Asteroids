using UnityEngine;
using VContainer;
using VContainer.Unity;

public class SceneScope : LifetimeScope
{
    [SerializeField] private KeyboardMonitor _keyboardMonitor;
    [SerializeField] private Camera _camera;

    [SerializeField] private PlayerKeyboardController _playerKeyboardController;
    [SerializeField] private PlayerMovementSettings _playerMovementSettings;
    [SerializeField] private PlayerCombatSettings _playerCombatSettings;
    [SerializeField] private Rigidbody2D _playerRigidBody;

    [SerializeField] private PrefabHolder _prefabHolder;
    [SerializeField] private SceneObjectHolder _sceneObjectHolder;
    [SerializeField] private BulletSpawner _bulletSpawner;
    [SerializeField] private Laser _playerLaser;

    [SerializeField] private ScreenBoundsTrackerSettings _screenBoundsTrackerSettings;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<SceneEntryPoint>();

        builder.RegisterComponent<KeyboardMonitor>(_keyboardMonitor);

        builder.RegisterComponent<Rigidbody2D>(_playerRigidBody);
        builder.RegisterComponent<PlayerMovementSettings>(_playerMovementSettings);
        builder.RegisterComponent<PlayerCombatSettings>(_playerCombatSettings);
        builder.RegisterComponent<PlayerKeyboardController>(_playerKeyboardController);
        builder.RegisterComponent<Camera>(_camera);
        builder.RegisterComponent<PrefabHolder>(_prefabHolder);
        builder.RegisterComponent<SceneObjectHolder>(_sceneObjectHolder);
        builder.RegisterComponent<BulletSpawner>(_bulletSpawner);
        builder.RegisterComponent<Laser>(_playerLaser);
        builder.RegisterComponent<ScreenBoundsTrackerSettings>(_screenBoundsTrackerSettings);

        builder.RegisterInstance<InertialMoverSettings>(_playerMovementSettings.MoverSettings);
        builder.RegisterInstance<DirectionalRotationSettings>(_playerMovementSettings.RotationSettings);

    }
}
