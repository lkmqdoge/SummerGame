using Microsoft.Xna.Framework;

namespace SummerGame.Core.Physics;

public readonly struct CircleShape
    : IShape
{
    public readonly IShape.ShapeType Type => IShape.ShapeType.Circle;
    public readonly Vector2 Position { get; }
    public readonly float Radius { get; }
}




