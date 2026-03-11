using System;
using _Project.Sources.Gameplay.ObjectMovement.Movers;
using _Project.Sources.Gameplay.ObjectMovement.Rotators;
using _Project.Sources.Gameplay.WeaponSystem;
using UnityEngine;
using VContainer;

namespace _Project.Sources.Input
{
    public class PlayerKeyboardController : IDisposable
    {
        private DirectionalRotator _playerRotator;
        private InertialMover _playerMover;
        private KeyboardMonitor _keyboardMonitor;

        private BulletSpawner _bulletSpawner;
        private Laser _laser;

        [Inject]
        public void Construct(KeyboardMonitor keyboardMonitor,
            BulletSpawner bulletSpawner, Laser laser)
        {
            _keyboardMonitor = keyboardMonitor;
            _bulletSpawner = bulletSpawner;
            _laser = laser;
        }

        public void Init(InertialMover inertialMover, DirectionalRotator directionalRotator)
        {
            _playerMover = inertialMover;
            _playerRotator = directionalRotator;
        }

        public void Tick()
        {
            _playerMover.Move();
            _playerRotator.Rotate();
        }

        public void KeyboardMonitorSubscribe()
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

        public void Unsubscribe()
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
        private void HandleForwardDown() => _playerMover.SetMoving(true);
        private void HandleForwardUp() => _playerMover.SetMoving(false);

        private void HandleBackwardDown() => _playerMover.SetBraking(true);
        private void HandleBackwardUp() => _playerMover.SetBraking(false);

        private void HandleLeftDown() => _playerRotator.SetRotateLeft(true);
        private void HandleLeftUp() => _playerRotator.SetRotateLeft(false);

        private void HandleRightDown() => _playerRotator.SetRotateRight(true);
        private void HandleRightUp() => _playerRotator.SetRotateRight(false);
        public void Dispose() => Unsubscribe();
    }
}