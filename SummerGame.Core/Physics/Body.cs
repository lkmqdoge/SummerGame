using System;
using Microsoft.Xna.Framework;

namespace SummerGame.Core.Physics;

public class Body
    : IBody
{
    public const float LowVelocityCap = 0.0001f;

    public Collider Collider { get; }
    public Vector2 Velocity { get; }
    public bool Sleeping { get; }

    public Collision CollideWithStaticBody(IStaticBody staticBody, float delta)
    {
        if (Velocity.X < LowVelocityCap && Velocity.Y < LowVelocityCap)
            return Collision.Empty;

        var staticRect = staticBody.Collider.Shape.Rect;

        var expandedRect = new Rectangle(
            staticRect.Location - (Collider.Shape.Rect.Size / 2),
            staticRect.Size + Collider.Shape.Rect.Size
        );

        return RayVsRect(Collider.Shape.Rect.Center.ToVector2(), Velocity*delta, expandedRect);
    }

    private Collision RayVsRect(Vector2 origin, Vector2 dir, Rectangle target)
    {
        var invDir = new Vector2(1.0f / dir.X, 1.0f / dir.Y);
        var tNear = (target.Location.ToVector2() - origin) * invDir;
        var tFar = (target.Location.ToVector2() + target.Size.ToVector2() - origin) * invDir;

        if (float.IsNaN(tFar.Y) || float.IsNaN(tFar.X) || float.IsNaN(tNear.Y) || float.IsNaN(tNear.Y))
            return Collision.Empty;

        if (tNear.X > tFar.X) (tFar.X, tNear.X) = (tNear.X, tFar.X);
        if (tNear.Y > tFar.Y) (tFar.Y, tNear.Y) = (tNear.Y, tFar.Y);

        if (tNear.X > tFar.Y || tNear.Y > tFar.X)
            return Collision.Empty;

        var tHitNear = Math.Max(tNear.X, tNear.Y);
        var tHitFar = Math.Min(tFar.X, tFar.Y);

        if (tHitFar < 0)
            return Collision.Empty;

        var contactPoint = origin + (dir * tHitNear);
        var contactNormal = Vector2.Zero;

        if (tNear.X > tNear.Y)
            contactNormal = new Vector2(Math.Sign(invDir.Y), 0);
        else if (tNear.X > tNear.Y)
            contactNormal = new Vector2(0, Math.Sign(invDir.Y));

        return new (){
            IsColliding = tHitNear >= 0.0f && tHitNear < 1.0f,
            ContactPoint = contactPoint,
            ContactNormal = contactNormal,
            ContactTime = tHitNear
        };
    }
}


