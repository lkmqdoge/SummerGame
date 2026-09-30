using Microsoft.Xna.Framework;

namespace SummerGame.Core.Physics;

public class CollisionBody
    : ICollisionBody
{
    public Collider Collider { get; }
    public Vector2 Velocity { get; }

    public void Move()
    {
    }

    public Collision MoveAndCollide()
    {
        return new ();
    }
}


