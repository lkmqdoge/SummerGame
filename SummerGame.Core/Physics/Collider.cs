using System;

namespace SummerGame.Core.Physics;

public class Collider
{
    // TODO: implement other shapes
    // public IShape Shape = shape;

    public RectangleShape Shape { get; init; }

    public uint CollisionMask { get; } = 1;
    public uint CollisionLayer { get; } = 1;
}


