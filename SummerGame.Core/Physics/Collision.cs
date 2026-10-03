using Microsoft.Xna.Framework;

namespace SummerGame.Core.Physics;

public readonly struct Collision
{
    public bool IsColliding { get; init; }
    public float ContactTime { get; init; }
    public Vector2 ContactNormal { get; init; }
    public Vector2 ContactPoint { get; init; }

    public static Collision Empty => new ()
    {
        IsColliding = false
    };
}


