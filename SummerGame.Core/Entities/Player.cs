using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerGame.Core.Graphics;

namespace SummerGame.Core.Entities;

public class Player(GameCore game)
    : Entity(game),
    Graphics.IDrawable
{
    public Vector2 Position { get; set; } = Vector2.Zero;
    public bool Visible { get; set; } = true;
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

    public void Draw(SpriteBatch spriteBatch)
    {
        _sprite.Draw(spriteBatch);
    }
}


