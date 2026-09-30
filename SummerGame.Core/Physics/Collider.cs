namespace SummerGame.Core.Physics;

public class Collider(IShape shape)
{
    public IShape Shape = shape;
    public uint CollisionMask { get; } = 1;
    public uint CollisionLayer { get; } = 1;

    public bool CheckCollision(Collider other)
    {

    }
}


