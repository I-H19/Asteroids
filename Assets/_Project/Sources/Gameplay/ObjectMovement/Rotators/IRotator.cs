namespace Asteroids.Gameplay.ObjectMovement
{
    public interface IRotator
    {
        public float CurrentAngle { get; }
        public void Rotate();
        public void SetEnabled(bool enabled);
    }
}