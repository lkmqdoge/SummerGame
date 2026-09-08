using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerGame.Core.Graphics;

namespace SummerGame.Core.Simulation;

public class Player(GameCore game) : GameObject(game)
{
    public Vector2 GlobalPosition { get; set; }

    public Sprite2D Sprite = new ();

    public override void LoadContent()
    {
        Sprite.Texture = Content.Load<Texture2D>("Textures/player");
    }

    public override void Update(double delta)
    {
    }
}


