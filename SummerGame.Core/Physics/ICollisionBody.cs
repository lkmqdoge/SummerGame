using Microsoft.Xna.Framework;

namespace SummerGame.Core.Physics;

public interface ICollisionBody
{
    ICollider Collider { get; }
    Vector2 Velocity { get; }

    void Move();
    ICollisionBody MoveAndCollide();
}


