using System;
using UnityEngine;
using VContainer;

namespace _Project.Sources.Gameplay.ObjectMovement
{
    public class ScreenBoundsTracker
    {
        private Camera _camera;
        private Rigidbody2D _rigidbody;

        private ScreenBounds _screenBounds;

        [Inject]
        public void Construct(Camera mainCamera) => _camera = mainCamera;

        public void InitBounds()
        {
            Vector3 cameraWorldPosition = _camera.transform.position;

            float halfScreenHeight = _camera.orthographicSize;
            float halfScreenWidth = halfScreenHeight * _camera.aspect;

            float leftScreenBorder = cameraWorldPosition.x - halfScreenWidth;
            float rightScreenBorder = cameraWorldPosition.x + halfScreenWidth;
            float bottomScreenBorder = cameraWorldPosition.y - halfScreenHeight;
            float topScreenBorder = cameraWorldPosition.y + halfScreenHeight;

            _screenBounds = new ScreenBounds(leftScreenBorder, rightScreenBorder, bottomScreenBorder, topScreenBorder);
        }
        public void Init(Rigidbody2D rigidbody)
        {
            _rigidbody = rigidbody;
            InitBounds();
        }
        public void Init(Rigidbody2D rigidbody, Camera mainCamera)
        {
            _camera = mainCamera;
            Init(rigidbody);
        }

        public bool IsOutOfBounds()
        {
            Vector2 position = _rigidbody.position;
            return _screenBounds.IsOutOfBounds(position);
        }

        public Vector2 GetTeleportPosition()
        {
            Vector2 position = _rigidbody.position;
            return _screenBounds.Wrap(position);
        }

        public Vector3 GetRandomPointOnPerimeter()
        {
            if (_screenBounds == null) throw new NullReferenceException();
            Vector2 randomVector2 = _screenBounds.GetRandomPointOnPerimeter();
            Vector3 randomVector3 = new(randomVector2.x, randomVector2.y, 0);
            return randomVector3;
        }
    }
}