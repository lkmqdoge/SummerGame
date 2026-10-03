using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerGame.Core.Graphics;

namespace SummerGame.Core.Entities;

public class Player(GameCore game)
    : Entity(game)
{
    public Vector2 Position { get; set; } = Vector2.Zero;
    public float Speed { get; set; } = 100f;
    public float SprintSpeed { get; set; } = 2000f;

    private readonly Sprite2D _sprite = new ();

    public override void LoadContent()
    {
        _sprite.Texture = Content.Load<Texture2D>("Textures/player");
    }

    public override void Update(double delta)
    {
        var am = Game.ActionManager;
        var dir = am.GetVector("move_left", "move_right", "move_up", "move_down");

        var speed = am.IsActionPressed("sprint") ? SprintSpeed : Speed;
        Position += dir * speed * (float)delta;
        _sprite.Position = Position;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        // _sprite.Draw(spriteBatch);
        spriteBatch.DrawRect(new Rectangle((Position - new Vector2(8, 8)).ToPoint(),
            new Vector2(16, 16).ToPoint()), new Color(1.0f, 0.0f, 0.0f, 0.3f));
    }
}


