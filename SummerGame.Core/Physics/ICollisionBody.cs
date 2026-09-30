using Microsoft.Xna.Framework;

namespace SummerGame.Core.Physics;

public interface ICollisionBody
{
    Collider Collider { get; }
    Vector2 Velocity { get; }

    void Move();
    Collision MoveAndCollide();
}


