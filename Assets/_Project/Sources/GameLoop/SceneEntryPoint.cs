using _Project.Sources.Gameplay;
using _Project.Sources.Gameplay.ObjectMovement;
using _Project.Sources.Gameplay.ObjectMovement.Movers;
using _Project.Sources.Gameplay.ObjectMovement.Rotators;
using _Project.Sources.Input;
using _Project.Sources.Settings.Movement;
using _Project.Sources.UI;
using TMPro.EditorUtilities;
using UnityEngine;
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
        private UIElementsUpdater _uiElementsUpdater;
        private PlayerMovementData _playerMovementData;
        private GameFinisher _gameFinisher;

        [Inject]
        public void Construct(Player player, PlayerMovementSettings playerMovementSettings, 
            ScreenBoundsTrackerSettings screenBoundsTrackerSettings, PlayerKeyboardController playerKeyboardController,
            UIElementsUpdater  uiElementsUpdater, PlayerMovementData playerMovementData, GameFinisher gameFinisher)
        {
            _player = player;
            _playerMovementSettings = playerMovementSettings;
            _playerMovementData = playerMovementData;
            _screenBoundsTrackerSettings = screenBoundsTrackerSettings;
            _playerKeyboardController = playerKeyboardController;
            _uiElementsUpdater =  uiElementsUpdater;
            _gameFinisher =  gameFinisher;
        }
        public void Initialize()
        {
            _player.Init();
            InitializePlayerMovement();
            InitializePlayerScreenBounds();
            
            _playerMovementData.Init(_player);
            _playerKeyboardController.Init(_player.InertialMoverTemplate, _player.DirectionalRotatorTemplate);

            _uiElementsUpdater.Subscribe();
            _playerKeyboardController.KeyboardMonitorSubscribe();

            _gameFinisher.Init(_player);
        }

        private void InitializePlayerScreenBounds()
        {
            ScreenBoundsTracker playerScreenBoundsTracker = _player.ScreenBoundsTrackerTemplate;
            playerScreenBoundsTracker.ChangeTrackingBounds(_screenBoundsTrackerSettings.PlayerBoundsMultiplier);
        }

        private void InitializePlayerMovement()
        {
            DirectionalRotator directionalRotator = _player.DirectionalRotatorTemplate;
            InertialMover inertialMover = _player.InertialMoverTemplate;

            directionalRotator.Init(_player.RigidBodyTemplate, _playerMovementSettings.RotationSettings);
            inertialMover.Init(_playerMovementSettings.MoverSettings, _player.RigidBodyTemplate);
        }
    }
}
