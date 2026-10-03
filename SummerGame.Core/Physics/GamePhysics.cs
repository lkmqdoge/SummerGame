using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerGame.Core.Graphics;

namespace SummerGame.Core.Physics;

public class GamePhysics
    : GameObject
{
    public List<IBody> Bodies { get; } = [];
    public List<IArea> Areas { get; } = [];
    public List<IStaticBody> StaticBodies { get; } = [];

    public GamePhysics(GameCore game) : base(game)
    {
        StaticBodies = [
            new StaticBody(){ Collider = new (){ Shape = new(){ Rect = new(-500, 80, 400, 200) }}},
            new StaticBody(){ Collider = new (){ Shape = new(){ Rect = new(50, 0, 400, 200) }}},
            new StaticBody(){ Collider = new (){ Shape = new(){ Rect = new(-260, -55, 200, 200) }}},
        ];
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (!Visible)
            return;

        foreach(var body in StaticBodies)
            spriteBatch.DrawRect(body.Collider.Shape.Rect, new Color(0, 1.0f, 0, 0.3f));
    }

    public void Update(float delta)
    {
        for (int i = 0; i < Bodies.Count; i++)
        {
            var body = Bodies[i];

            if (body.Sleeping)
                continue;

            var collisions = new List<Collision>();

            foreach (var staticBody in StaticBodies)
            {
                var collision = body.CollideWithStaticBody(staticBody, delta);
                collisions.Add(collision);
            }

            collisions.Sort((a, b) => a.ContactTime.CompareTo(b.ContactTime));
        }
    }
}


