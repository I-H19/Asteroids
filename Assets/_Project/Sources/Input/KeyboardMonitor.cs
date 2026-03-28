using System;
using UnityEngine;

namespace _Project.Sources.Input
{
    public class KeyboardMonitor
    {
        public event Action ForwardButtonDown;
        public event Action ForwardButtonUp;

        public event Action BackwardButtonDown;
        public event Action BackwardButtonUp;

        public event Action LeftButtonDown;
        public event Action LeftButtonUp;

        public event Action RightButtonDown;
        public event Action RightButtonUp;

        public event Action ShootButtonDown;
        public event Action ShootButtonUp;

        public event Action LaserButtonDown;

        public void Tick()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.W)) ForwardButtonDown?.Invoke();
            if (UnityEngine.Input.GetKeyUp(KeyCode.W)) ForwardButtonUp?.Invoke();

            if (UnityEngine.Input.GetKeyDown(KeyCode.S)) BackwardButtonDown?.Invoke();
            if (UnityEngine.Input.GetKeyUp(KeyCode.S)) BackwardButtonUp?.Invoke();

            if (UnityEngine.Input.GetKeyDown(KeyCode.A)) LeftButtonDown?.Invoke();
            if (UnityEngine.Input.GetKeyUp(KeyCode.A)) LeftButtonUp?.Invoke();

            if (UnityEngine.Input.GetKeyDown(KeyCode.D)) RightButtonDown?.Invoke();
            if (UnityEngine.Input.GetKeyUp(KeyCode.D)) RightButtonUp?.Invoke();

            if (UnityEngine.Input.GetKeyDown(KeyCode.Space) || UnityEngine.Input.GetKeyDown(KeyCode.Mouse0)) ShootButtonDown?.Invoke();
            if (UnityEngine.Input.GetKeyUp(KeyCode.Space) || UnityEngine.Input.GetKeyUp(KeyCode.Mouse0)) ShootButtonUp?.Invoke();

            if (UnityEngine.Input.GetKeyDown(KeyCode.LeftControl) || UnityEngine.Input.GetKeyDown(KeyCode.Mouse1)) LaserButtonDown?.Invoke();
        }
    }
}

