using UnityEngine;
using VContainer;
using VContainer.Unity;

public class SceneEntryPoint : IInitializable
{
    private GameObject _player;
    private PlayerMovementSettings _playerMovementSettings;
    private ScreenBoundsTrackerSettings _screenBoundsTrackerSettings;

    [Inject]
    public void Construct(SceneObjectHolder sceneObjectHolder, PlayerMovementSettings playerMovementSettings, ScreenBoundsTrackerSettings screenBoundsTrackerSettings)
    {
        _player = sceneObjectHolder.Player;
        _playerMovementSettings = playerMovementSettings;
        _screenBoundsTrackerSettings = screenBoundsTrackerSettings;
    }
    public void Initialize()
    {
        DirectionalRotator directionalRotator = _player.GetComponent<DirectionalRotator>();
        InertialMover inertialMover = _player.GetComponent<InertialMover>();

        directionalRotator.Init(_player.GetComponent<Rigidbody2D>(), _playerMovementSettings.RotationSettings);
        inertialMover.Init(_player.GetComponent<Rigidbody2D>(), _playerMovementSettings.MoverSettings);

        ScreenBoundsTracker _playerScreenBoundsTracker = _player.GetComponent<ScreenBoundsTracker>();
        _playerScreenBoundsTracker.ChangeTrackingBounds(_screenBoundsTrackerSettings.PlayerBoundsMultiplier);
    }
}

