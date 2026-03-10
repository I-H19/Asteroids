namespace _Project.Sources.Gameplay.ObjectMovement.Rotators
{
    public interface IRotator
    {
        public float CurrentAngle { get; }
        public void Rotate();
        public void SetEnabled(bool enabled);
    }
}