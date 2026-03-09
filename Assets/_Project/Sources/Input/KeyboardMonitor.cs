using System;
using UnityEngine;

namespace Asteroids.PlayerInput
{
    public class KeyboardMonitor
    {
        public Action ForwardButtonDown;
        public Action ForwardButtonUp;

        public Action BackwardButtonDown;
        public Action BackwardButtonUp;

        public Action LeftButtonDown;
        public Action LeftButtonUp;

        public Action RightButtonDown;
        public Action RightButtonUp;

        public Action ShootButtonDown;
        public Action ShootButtonUp;

        public Action LaserButtonDown;

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.W)) ForwardButtonDown?.Invoke();
            if (Input.GetKeyUp(KeyCode.W)) ForwardButtonUp?.Invoke();

            if (Input.GetKeyDown(KeyCode.S)) BackwardButtonDown?.Invoke();
            if (Input.GetKeyUp(KeyCode.S)) BackwardButtonUp?.Invoke();

            if (Input.GetKeyDown(KeyCode.A)) LeftButtonDown?.Invoke();
            if (Input.GetKeyUp(KeyCode.A)) LeftButtonUp?.Invoke();

            if (Input.GetKeyDown(KeyCode.D)) RightButtonDown?.Invoke();
            if (Input.GetKeyUp(KeyCode.D)) RightButtonUp?.Invoke();

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)) ShootButtonDown?.Invoke();
            if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Mouse0)) ShootButtonUp?.Invoke();

            if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.Mouse1)) LaserButtonDown?.Invoke();
        }
    }
}

