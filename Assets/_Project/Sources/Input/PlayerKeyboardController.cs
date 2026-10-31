using System;
using VContainer;

namespace Asteroids
{
    public class PlayerKeyboardController : IDisposable, IPausable, ISceneFixedTickable
    {
        private BulletSpawner _bulletSpawner;
        private bool _isPause;
        private KeyboardMonitor _keyboardMonitor;
        private Laser _laser;
        private InertialMover _playerMover;
        private DirectionalRotator _playerRotator;

        public void Dispose()
        {
            KeyboardUnsubscribe();
        }

        public void Pause()
        {
            _isPause = true;
        }

        public void Unpause()
        {
            _isPause = false;
        }

        public void FixedTick()
        {
            if (_isPause)
            {
                return;
            }

            _playerMover.Move();
            _playerRotator.Rotate();
        }

        [Inject]
        public void Construct(KeyboardMonitor keyboardMonitor)
        {
            _keyboardMonitor = keyboardMonitor;
        }

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

        public void KeyboardSubscribe()
        {
            _keyboardMonitor.ForwardButtonDown += OnForwardButtonDown;
            _keyboardMonitor.ForwardButtonUp += OnForwardButtonUp;

            _keyboardMonitor.BackwardButtonDown += OnBackwardButtonDown;
            _keyboardMonitor.BackwardButtonUp += OnBackwardButtonUp;

            _keyboardMonitor.LeftButtonDown += OnLeftButtonDown;
            _keyboardMonitor.LeftButtonUp += OnLeftButtonUp;

            _keyboardMonitor.RightButtonDown += OnRightButtonDown;
            _keyboardMonitor.RightButtonUp += OnRightButtonUp;

            _keyboardMonitor.ShootButtonDown += OnShootButtonDown;
            _keyboardMonitor.ShootButtonUp += OnShootButtonUp;

            _keyboardMonitor.LaserButtonDown += OnLaserButtonDown;
        }

        private void KeyboardUnsubscribe()
        {
            _keyboardMonitor.ForwardButtonDown -= OnForwardButtonDown;
            _keyboardMonitor.ForwardButtonUp -= OnForwardButtonUp;

            _keyboardMonitor.BackwardButtonDown -= OnBackwardButtonDown;
            _keyboardMonitor.BackwardButtonUp -= OnBackwardButtonUp;

            _keyboardMonitor.LeftButtonDown -= OnLeftButtonDown;
            _keyboardMonitor.LeftButtonUp -= OnLeftButtonUp;

            _keyboardMonitor.RightButtonDown -= OnRightButtonDown;
            _keyboardMonitor.RightButtonUp -= OnRightButtonUp;

            _keyboardMonitor.ShootButtonDown -= OnShootButtonDown;
            _keyboardMonitor.ShootButtonUp -= OnShootButtonUp;

            _keyboardMonitor.LaserButtonDown -= OnLaserButtonDown;
        }

        private void OnLaserButtonDown()
        {
            _laser.TryShoot();
        }

        private void OnShootButtonDown()
        {
            _bulletSpawner.StartSpawning();
        }

        private void OnShootButtonUp()
        {
            _bulletSpawner.StopSpawning();
        }

        private void OnForwardButtonDown()
        {
            _playerMover.SetAccelerating(true);
        }

        private void OnForwardButtonUp()
        {
            _playerMover.SetAccelerating(false);
        }

        private void OnBackwardButtonDown()
        {
            _playerMover.SetBraking(true);
        }

        private void OnBackwardButtonUp()
        {
            _playerMover.SetBraking(false);
        }

        private void OnLeftButtonDown()
        {
            _playerRotator.SetRotateLeft(true);
        }

        private void OnLeftButtonUp()
        {
            _playerRotator.SetRotateLeft(false);
        }

        private void OnRightButtonDown()
        {
            _playerRotator.SetRotateRight(true);
        }

        private void OnRightButtonUp()
        {
            _playerRotator.SetRotateRight(false);
        }
    }
}
