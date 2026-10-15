using System;
using _Project.Sources.GameLoop;
using _Project.Sources.Gameplay.ObjectMovement.Movers;
using _Project.Sources.Gameplay.ObjectMovement.Rotators;
using _Project.Sources.Gameplay.WeaponSystem;
using VContainer;

namespace _Project.Sources.Input
{
    public class PlayerKeyboardController : IDisposable, IPausable, ISceneTickable
    {
        private DirectionalRotator _playerRotator;
        private InertialMover _playerMover;
        private KeyboardMonitor _keyboardMonitor;

        private BulletSpawner _bulletSpawner;
        private Laser _laser;
        private bool _isPause;

        [Inject]
        public void Construct(KeyboardMonitor keyboardMonitor) => _keyboardMonitor = keyboardMonitor;

        public void Init(
            InertialMover inertialMover,
            DirectionalRotator directionalRotator,
            BulletSpawner bulletSpawner,
            Laser laser)
        {
            _playerMover = inertialMover;
            _playerRotator = directionalRotator;
            _bulletSpawner = bulletSpawner;
            _laser = laser;
        }

        public void Tick()
        {
            if (_isPause) return;
            
            _playerMover.Move();
            _playerRotator.Rotate();
        }

        public void KeyboardSubscribe()
        {
            _keyboardMonitor.ForwardButtonDown += HandleForwardDown;
            _keyboardMonitor.ForwardButtonUp += HandleForwardUp;

            _keyboardMonitor.BackwardButtonDown += HandleBackwardDown;
            _keyboardMonitor.BackwardButtonUp += HandleBackwardUp;

            _keyboardMonitor.LeftButtonDown += HandleLeftDown;
            _keyboardMonitor.LeftButtonUp += HandleLeftUp;

            _keyboardMonitor.RightButtonDown += HandleRightDown;
            _keyboardMonitor.RightButtonUp += HandleRightUp;

            _keyboardMonitor.ShootButtonDown += HandleShootDown;
            _keyboardMonitor.ShootButtonUp += HandleShootUp;

            _keyboardMonitor.LaserButtonDown += HandleLaserDown;
        }

        private void KeyboardUnsubscribe()
        {
            _keyboardMonitor.ForwardButtonDown -= HandleForwardDown;
            _keyboardMonitor.ForwardButtonUp -= HandleForwardUp;

            _keyboardMonitor.BackwardButtonDown -= HandleBackwardDown;
            _keyboardMonitor.BackwardButtonUp -= HandleBackwardUp;

            _keyboardMonitor.LeftButtonDown -= HandleLeftDown;
            _keyboardMonitor.LeftButtonUp -= HandleLeftUp;

            _keyboardMonitor.RightButtonDown -= HandleRightDown;
            _keyboardMonitor.RightButtonUp -= HandleRightUp;

            _keyboardMonitor.ShootButtonDown -= HandleShootDown;
            _keyboardMonitor.ShootButtonUp -= HandleShootUp;

            _keyboardMonitor.LaserButtonDown -= HandleLaserDown;
        }

        private void HandleLaserDown() => _laser.TryShoot();
        private void HandleShootDown() => _bulletSpawner.StartSpawning();
        private void HandleShootUp() => _bulletSpawner.StopSpawning();
        private void HandleForwardDown() => _playerMover.StartAcceleration(true);
        private void HandleForwardUp() => _playerMover.StartAcceleration(false);

        private void HandleBackwardDown() => _playerMover.StartBraking(true);
        private void HandleBackwardUp() => _playerMover.StartBraking(false);

        private void HandleLeftDown() => _playerRotator.SetRotateLeft(true);
        private void HandleLeftUp() => _playerRotator.SetRotateLeft(false);

        private void HandleRightDown() => _playerRotator.SetRotateRight(true);
        private void HandleRightUp() => _playerRotator.SetRotateRight(false);
        public void Dispose() => KeyboardUnsubscribe();
        public void Pause() => _isPause = true;
        public void Unpause() => _isPause = false;
    }
}
