using Microsoft.Xna.Framework;

namespace SummerGame.Core.Physics;

public readonly struct Collision
{
    public readonly float ContactTime { get; }
    public readonly Vector2 ContactNormal { get; }
}


