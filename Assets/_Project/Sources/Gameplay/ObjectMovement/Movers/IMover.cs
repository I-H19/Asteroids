using _Project.Sources.Config.Movement;
using UnityEngine;

namespace _Project.Sources.Gameplay.ObjectMovement.Movers
{
    public interface IMover
    {
        public void Init(IMoverSettings settings, Rigidbody2D moveRigidbody);
        public void Move();
        public void SetMoving(bool value);
        public void SetBraking(bool value);
        public void SetEnabled(bool enabled);
        public void SetPosition(Vector2 position);
    }
}