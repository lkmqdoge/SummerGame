namespace SummerGame.Core.Physics;

public interface IShape
{
    ShapeType Type { get; }

    public enum ShapeType
    {
        Rectangle,
        Point,
        Circle,
        Ray
    }
}


