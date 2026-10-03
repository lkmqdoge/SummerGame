
using Microsoft.Xna.Framework;

namespace SummerGame.Core.Physics;

public readonly struct PointShape
    : IShape
{
    public readonly IShape.ShapeType Type => IShape.ShapeType.Rectangle;
    public readonly Vector2 Position { get; }
}




