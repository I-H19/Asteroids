using UnityEngine;
using VContainer;
using VContainer.Unity;

public class MainSceneScope : LifetimeScope
{
    [SerializeField] private KeyboardMonitor _keyboardMonitor;

    [SerializeField] private PlayerKeyboardController _playerKeyboardController;
    [SerializeField] private PlayerMovementSettings _playerSettings;
    [SerializeField] private Rigidbody2D _playerRigidBody;
    [SerializeField] private InertialMover _playerInertialMover;
    [SerializeField] private DirectionalRotator _playerDirectionalRotator;

    protected override void Configure(IContainerBuilder builder)
    { 
        builder.RegisterComponent<KeyboardMonitor>(_keyboardMonitor);

        builder.RegisterComponent<Rigidbody2D>(_playerRigidBody);
        builder.RegisterComponent<PlayerMovementSettings>(_playerSettings);
        builder.RegisterComponent<PlayerKeyboardController>(_playerKeyboardController);
        builder.RegisterComponent<InertialMover>(_playerInertialMover);
        builder.RegisterComponent<DirectionalRotator>(_playerDirectionalRotator);

        builder.RegisterInstance<InertialMoverSettings>(_playerSettings.MoverSettings);
        builder.RegisterInstance<DirectionalRotationSettings>(_playerSettings.RotationSettings);

    }
}
