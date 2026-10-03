using Microsoft.Xna.Framework;

namespace SummerGame.Core.Physics;

public interface IBody
{
    Collider Collider { get; }
    Vector2 Velocity { get; }

    bool Sleeping { get; }

    Collision CollideWithStaticBody(IStaticBody staticBody, float delta);
}


