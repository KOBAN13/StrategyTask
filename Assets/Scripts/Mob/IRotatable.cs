namespace Mob
{
    public interface IRotatable : ICurrentState
    {
        void Rotate();
        float RotationSpeed { get; }
    }
}