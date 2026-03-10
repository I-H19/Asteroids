using UnityEngine;

namespace _Project.Sources.Gameplay.ObjectMovement.Rotators
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class LookAtRotator : MonoBehaviour, IRotator
    {
        public float CurrentAngle { get; private set; }

        [SerializeField] private Transform _target;
        private Rigidbody2D _rigidBody;
        private bool _enabled = true;
        private readonly float _minTargetDistanceSqr = 0.000001f;
        public void Init(Transform lookAtTarget)
        {
            _target = lookAtTarget;
            _rigidBody = GetComponent<Rigidbody2D>();
        }

        public void Rotate()
        {
            if (!_enabled) return;
            LookAt(_rigidBody, _target.transform.position, Vector2.up);
        }

        private void LookAt(Rigidbody2D rigidbody, Vector2 targetPosition, Vector2 forwardAxis)
        {
            Vector2 toTarget = targetPosition - rigidbody.position;
            if (toTarget.sqrMagnitude < _minTargetDistanceSqr)
            {
                return;
            }

            float targetAngle = Vector2.SignedAngle(forwardAxis, toTarget);
            rigidbody.MoveRotation(targetAngle);
        }

        public void SetEnabled(bool enabled)
        {
            _enabled = enabled;
            if (!_enabled) _rigidBody.angularVelocity = 0f;
        }
    }
}

