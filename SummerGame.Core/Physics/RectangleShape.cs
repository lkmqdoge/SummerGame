using Microsoft.Xna.Framework;

namespace SummerGame.Core.Physics;

public readonly struct RectangleShape
    : IShape
{
    public readonly IShape.ShapeType Type => IShape.ShapeType.Rectangle;
    public readonly Rectangle Rect { get; }
}


