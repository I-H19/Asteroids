using _Project.Sources.Config;
using _Project.Sources.Config.Movement;
using _Project.Sources.Gameplay;
using _Project.Sources.Gameplay.EnemySystem.EnemySpawn.EnemyFactory;
using _Project.Sources.Gameplay.ObjectMovement;
using _Project.Sources.Gameplay.ObjectMovement.Movers;
using _Project.Sources.Gameplay.ObjectMovement.Rotators;
using _Project.Sources.Gameplay.WeaponSystem;
using _Project.Sources.Input;
using _Project.Sources.UI;
using _Project.Sources.UI.GUI;
using _Project.Sources.UI.MVP;
using TMPro;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Sources.GameLoop
{
    public class SceneEntryPoint : IInitializable
    {
        private Player _player;
        private PlayerMovementSettings _playerMovementSettings;
        private PlayerKeyboardController _playerKeyboardController;

        private GameFinisher _gameFinisher;
        private Presenter _presenter;
        private PrefabHolder _prefabHolder;
        private IObjectResolver _resolver;
        private GamePause _gamePause;
        private Laser _laser;
        private GameObject _laserVisual;
        private BulletSpawner _bulletSpawner;
        private GameRestarter _gameRestarter;
        private SceneTickDriver _sceneTickDriver;
        private UfoFactory _ufoFactory;

        [Inject]
        public void Construct(IObjectResolver resolver, PlayerMovementSettings playerMovementSettings,
            PlayerKeyboardController playerKeyboardController,
            GameFinisher gameFinisher, PrefabHolder prefabHolder,
            GamePause gamePause, GameRestarter gameRestarter, SceneTickDriver sceneTickDriver, UfoFactory ufoFactory)
        {
            _resolver = resolver;
            _playerMovementSettings = playerMovementSettings;
            _playerKeyboardController = playerKeyboardController;
            _gameFinisher = gameFinisher;
            _gamePause = gamePause;
            _prefabHolder = prefabHolder;
            _gameRestarter = gameRestarter;
            _sceneTickDriver = sceneTickDriver;
            _ufoFactory = ufoFactory;
        }

        public void Initialize()
        {
            ConstructPlayer();
            _player.Init(_laser, _bulletSpawner);

            InitializePlayerMovement();

            ConstructUI();

            InitializePlayerScreenBounds();

            _playerKeyboardController.Init(_player.InertialMoverTemplate, _player.DirectionalRotatorTemplate,
                _bulletSpawner, _laser);
            _playerKeyboardController.KeyboardMonitorSubscribe();

            _gameRestarter.Init(_player);
            _gamePause.Init(_player, _bulletSpawner);
            _gameFinisher.Init(_player);
            _sceneTickDriver.Init(_player, _bulletSpawner);
            
            _ufoFactory.Init(_player);
        }

        private void InitializePlayerScreenBounds()
        {
            ScreenBoundsTracker playerScreenBoundsTracker = _player.ScreenBoundsTrackerTemplate;
            playerScreenBoundsTracker.Init();
        }

        private void InitializePlayerMovement()
        {
            DirectionalRotator directionalRotator = _player.DirectionalRotatorTemplate;
            InertialMover inertialMover = _player.InertialMoverTemplate;

            directionalRotator.Init(_player.RigidBodyTemplate, _playerMovementSettings.RotationSettings);
            inertialMover.Init(_playerMovementSettings.MoverSettings, _player.RigidBodyTemplate);
        }

        private void ConstructPlayer()
        {
            GameObject playerRoot = _resolver.Instantiate(_prefabHolder.PlayerRoot);
            _player = playerRoot.GetComponent<Player>();

            GameObject playerVisual = _resolver.Instantiate(_prefabHolder.PlayerVisual, playerRoot.transform);

            GameObject laser = _resolver.Instantiate(_prefabHolder.Laser, playerVisual.transform);
            _laser = laser.GetComponent<Laser>();

            _laserVisual = _resolver.Instantiate(_prefabHolder.LaserVisual, laser.transform);
            _laser.Init(_laserVisual);

            _bulletSpawner = new BulletSpawner();
            _resolver.Inject(_bulletSpawner);
            _bulletSpawner.Init(_player);
        }

        private void ConstructUI()
        {
            GameObject uiRoot = new("[UI]");
            GameObject rootCanvas = _resolver.Instantiate(_prefabHolder.RootCanvas, uiRoot.transform);
            GameObject runtimeScore = _resolver.Instantiate(_prefabHolder.PlayerRuntimeScore, rootCanvas.transform);
            GameObject playerStatsView = _resolver.Instantiate(_prefabHolder.PlayerStats, rootCanvas.transform);
            _resolver.Instantiate(_prefabHolder.EventSystem, uiRoot.transform);

            GameObject restartPanel = _resolver.Instantiate(_prefabHolder.RestartPanel, rootCanvas.transform);
            GameObject resultScore = _resolver.Instantiate(_prefabHolder.PlayerResultScore, restartPanel.transform);
            GameObject restartButton = _resolver.Instantiate(_prefabHolder.RestartGameButton, restartPanel.transform);

            Presenter presenter = new();
            _resolver.Inject(presenter);

            RestartButton restartButtonComponent = restartButton.GetComponent<RestartButton>();
            View view = rootCanvas.GetComponent<View>();
            
            PlayerStats playerStatsModel = new();
            Model model = new(playerStatsModel);

            restartButtonComponent.Init();

            view.Init(runtimeScore.GetComponent<TextMeshProUGUI>(), resultScore.GetComponent<TextMeshProUGUI>(),
                playerStatsView.GetComponent<TextMeshProUGUI>(), restartPanel);
            presenter.Init(model, view, restartButtonComponent, _player);
        }
    }
}