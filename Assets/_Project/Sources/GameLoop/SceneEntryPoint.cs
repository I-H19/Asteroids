using _Project.Sources.Config;
using _Project.Sources.Config.Movement;
using _Project.Sources.Gameplay;
using _Project.Sources.Gameplay.EnemySystem.EnemySpawn.EnemyFactory;
using _Project.Sources.Gameplay.ObjectMovement.Movers;
using _Project.Sources.Gameplay.ObjectMovement.Rotators;
using _Project.Sources.Gameplay.WeaponSystem;
using _Project.Sources.Input;
using _Project.Sources.UI;
using _Project.Sources.UI.GameMVP;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
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
        private LaserVisual _laserVisual;
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
            _player.Init(_laser);

            InitializePlayerMovement();

            ConstructUI();
            
            _playerKeyboardController.Init(_player.InertialMoverTemplate, _player.DirectionalRotatorTemplate,
                _bulletSpawner, _laser);
            _playerKeyboardController.KeyboardMonitorSubscribe();

            _gameRestarter.Init(_player);
            _gamePause.Init(_player, _bulletSpawner);
            _gameFinisher.Init(_player);
            _sceneTickDriver.Init(_player, _bulletSpawner);
            
            _ufoFactory.Init(_player);
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
            _player = _resolver.Instantiate(_prefabHolder.PlayerRoot);
            
            SpriteRenderer playerVisual = _resolver.Instantiate(_prefabHolder.PlayerVisual, _player.transform);
            
            _laser = _resolver.Instantiate(_prefabHolder.LaserTemplate, playerVisual.transform);

            _laserVisual = _resolver.Instantiate(_prefabHolder.LaserVisualTemplate, _laser.transform);
            _laser.Init(_laserVisual);

            _bulletSpawner = new BulletSpawner();
            _resolver.Inject(_bulletSpawner);
            _bulletSpawner.Init(_player);
        }

        private void ConstructUI()
        {
            GameObject uiRoot = new("[UI]");
            Canvas rootCanvas = _resolver.Instantiate(_prefabHolder.RootCanvas, uiRoot.transform);
            TextMeshProUGUI runtimeScore = _resolver.Instantiate(_prefabHolder.PlayerRuntimeScore, rootCanvas.transform);
            TextMeshProUGUI playerStatsView = _resolver.Instantiate(_prefabHolder.PlayerStats, rootCanvas.transform);
            _resolver.Instantiate(_prefabHolder.EventSystemTemplate, uiRoot.transform);

            Image restartPanel = _resolver.Instantiate(_prefabHolder.RestartPanel, rootCanvas.transform);
            TextMeshProUGUI resultScore = _resolver.Instantiate(_prefabHolder.PlayerResultScore, restartPanel.transform);
            Button restartButton = _resolver.Instantiate(_prefabHolder.RestartGameButton, restartPanel.transform);

            Presenter presenter = new();
            _resolver.Inject(presenter);

            View view = rootCanvas.GetComponent<View>();
            
            PlayerStats playerStatsModel = new();
            Model model = new(playerStatsModel);
            
            view.Init(runtimeScore, resultScore,
                playerStatsView, restartPanel.gameObject);
            presenter.Init(model, view, _player, restartButton);
        }
    }
}