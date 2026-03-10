using _Project.Sources.Gameplay;
using _Project.Sources.Gameplay.ObjectMovement;
using _Project.Sources.Gameplay.ObjectMovement.Movers;
using _Project.Sources.Gameplay.ObjectMovement.Rotators;
using _Project.Sources.Input;
using _Project.Sources.Settings;
using _Project.Sources.Settings.Movement;
using VContainer;
using VContainer.Unity;

namespace _Project.Sources.GameLoop
{
    public class SceneEntryPoint : IInitializable
    {
        private Player _player;
        private PlayerMovementSettings _playerMovementSettings;
        private ScreenBoundsTrackerSettings _screenBoundsTrackerSettings;
        private PlayerKeyboardController _playerKeyboardController;

        [Inject]
        public void Construct(SceneObjectHolder sceneObjectHolder, PlayerMovementSettings playerMovementSettings, ScreenBoundsTrackerSettings screenBoundsTrackerSettings, PlayerKeyboardController playerKeyboardController)
        {
            _player = sceneObjectHolder.Player.GetComponent<Player>();
            _playerMovementSettings = playerMovementSettings;
            _screenBoundsTrackerSettings = screenBoundsTrackerSettings;
            _playerKeyboardController = playerKeyboardController;
        }
        public void Initialize()
        {
            InitializePlayer();
        }

        private void InitializePlayer()
        {
            _player.Init();

            DirectionalRotator directionalRotator = _player.DirectionalRotatorTemplate;
            InertialMover inertialMover = _player.InertialMoverTemplate;

            directionalRotator.Init(_player.RigidBodyTemplate, _playerMovementSettings.RotationSettings);
            inertialMover.Init(_playerMovementSettings.MoverSettings, _player.RigidBodyTemplate);

            ScreenBoundsTracker _playerScreenBoundsTracker = _player.ScreenBoundsTrackerTemplate;
            _playerScreenBoundsTracker.ChangeTrackingBounds(_screenBoundsTrackerSettings.PlayerBoundsMultiplier);

            _playerKeyboardController.KeyboardMonitorSubscribe();
        }
    }
}
