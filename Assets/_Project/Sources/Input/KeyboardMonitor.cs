using System;
using UnityEngine;

namespace _Project.Sources.Input
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

