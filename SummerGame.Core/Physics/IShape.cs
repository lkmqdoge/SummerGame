namespace SummerGame.Core.Physics;

public interface IShape
{
    ShapeType Type { get; }
    ICollision Intersects(IShape other);
}

public enum ShapeType
{
    Rectangle,
    Point,
    Circle,
    Ray
}

