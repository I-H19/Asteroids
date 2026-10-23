using UnityEngine;

namespace Asteroids
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class LookAtRotator : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        private const float MIN_TARGET_DISTANCE_SQR = 0.000001f;
        private Rigidbody2D _rigidBody;

        public void Rotate()
        {
            LookAt(_rigidBody, _target.position, Vector2.up);
        }
        

        public void Init(Transform lookAtTarget)
        {
            _target = lookAtTarget;
            _rigidBody = GetComponent<Rigidbody2D>();
        }

        private static void LookAt(Rigidbody2D rigidBody, Vector2 targetPosition, Vector2 forwardAxis)
        {
            Vector2 toTarget = targetPosition - rigidBody.position;
            if (toTarget.sqrMagnitude < MIN_TARGET_DISTANCE_SQR)
            {
                return;
            }

            float targetAngle = Vector2.SignedAngle(forwardAxis, toTarget);
            rigidBody.MoveRotation(targetAngle);
        }
    }
}