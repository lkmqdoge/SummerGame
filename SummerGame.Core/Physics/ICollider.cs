namespace SummerGame.Core.Physics;

public interface ICollider
{
    IShape Shape { get; }

    uint CollisionMask { get; }
    uint CollisionLayer { get; }
}


