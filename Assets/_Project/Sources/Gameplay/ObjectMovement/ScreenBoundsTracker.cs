using System;
using UnityEngine;
using VContainer;

namespace _Project.Sources.Gameplay.ObjectMovement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class ScreenBoundsTracker : MonoBehaviour
    {
        private readonly float _defaultBoundsMultiplier = 1f;

        private Camera _camera;
        private Rigidbody2D _rigidbody;

        private ScreenBounds _screenBounds;

        [Inject]
        public void Construct(Camera mainCamera)
        {
            _camera = mainCamera;
            _rigidbody = GetComponent<Rigidbody2D>();
            CalculateScreenBounds(_defaultBoundsMultiplier);
        }

        public void ChangeTrackingBounds(float multiplier)
        {
            CalculateScreenBounds(multiplier);
        }

        private void CalculateScreenBounds(float multiplier)
        {
            if (multiplier < 1f)
                multiplier = 1f;

            Vector3 cameraWorldPosition = _camera.transform.position;

            float halfScreenHeight = _camera.orthographicSize;
            float halfScreenWidth = halfScreenHeight * _camera.aspect;

            halfScreenHeight *= multiplier;
            halfScreenWidth *= multiplier;

            float leftScreenBorder = cameraWorldPosition.x - halfScreenWidth;
            float rightScreenBorder = cameraWorldPosition.x + halfScreenWidth;
            float bottomScreenBorder = cameraWorldPosition.y - halfScreenHeight;
            float topScreenBorder = cameraWorldPosition.y + halfScreenHeight;

            _screenBounds = new ScreenBounds(leftScreenBorder, rightScreenBorder, bottomScreenBorder, topScreenBorder);
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