using Asteroids.Gameplay;
using Asteroids.Gameplay.ObjectMovement;
using System;
using UnityEngine;
using VContainer;

namespace Asteroids.UI
{
    public class PlayerMovementData : MonoBehaviour
    {
        public Action Changed;
        public float CurrentAngle { get; private set; }
        public float CurrentSpeed { get; private set; }

        private InertialMover _playerMover;
        private DirectionalRotator _playerRotator;

        [Inject]
        public void Construct(Player player)
        {
            _playerMover = player.InertialMoverTemplate;
            _playerRotator = player.DirectionalRotatorTemplate;
            Subscribe();
        }

        public void Subscribe()
        {
            _playerMover.SpeedChanged += OnSpeedChanged;
            _playerRotator.AngleChanged += OnAngleChanged;
        }


        public void Unsubscribe()
        {
            _playerMover.SpeedChanged -= OnSpeedChanged;
            _playerRotator.AngleChanged -= OnAngleChanged;
        }
        public void OnDestroy() => Unsubscribe();

        private void OnAngleChanged()
        {
            CurrentAngle = _playerRotator.CurrentAngle;
            Changed?.Invoke();
        }
        private void OnSpeedChanged()
        {
            CurrentSpeed = _playerMover.CurrentSpeed;
            Changed?.Invoke();
        }
    }
}