using System;
using VContainer;

namespace Asteroids
{
    public class PlayerKeyboardController : IDisposable
    {
        private BulletFiringController _bulletFiringController;
        private KeyboardMonitor _keyboardMonitor;
        private Player _player;

        [Inject]
        public void Construct(KeyboardMonitor keyboardMonitor)
        {
            _keyboardMonitor = keyboardMonitor;
        }

        public void Init(Player player, BulletFiringController bulletFiringController)
        {
            _player = player;
            _bulletFiringController = bulletFiringController;
        }

        public void Dispose()
        {
            KeyboardUnsubscribe();
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
            _player.TryLaserShoot();
        }

        private void OnShootButtonDown()
        {
            _bulletFiringController.StartFiring();
        }

        private void OnShootButtonUp()
        {
            _bulletFiringController.StopFiring();
        }

        private void OnForwardButtonDown()
        {
            _player.SetAccelerating(true);
        }

        private void OnForwardButtonUp()
        {
            _player.SetAccelerating(false);
        }

        private void OnBackwardButtonDown()
        {
            _player.SetBraking(true);
        }

        private void OnBackwardButtonUp()
        {
            _player.SetBraking(false);
        }

        private void OnLeftButtonDown()
        {
            _player.SetRotateLeft(true);
        }

        private void OnLeftButtonUp()
        {
            _player.SetRotateLeft(false);
        }

        private void OnRightButtonDown()
        {
            _player.SetRotateRight(true);
        }

        private void OnRightButtonUp()
        {
            _player.SetRotateRight(false);
        }
    }
}