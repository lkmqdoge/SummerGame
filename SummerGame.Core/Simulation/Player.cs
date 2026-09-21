using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerGame.Core.Graphics;
using SummerGame.Core.Input;

namespace SummerGame.Core.Simulation;

public class Player(GameCore game) : GameObject(game)
{
    // public Vector2 GlobalPosition { get; set; }
    // public float Speed { get; set; } = 10;
    //
    // public Sprite2D Sprite = new ();
    //
    // private ActionManager _actionManager = game.Simulation.ActionManager;
    //
    // public override void LoadContent()
    // {
    //     Sprite.Texture = Content.Load<Texture2D>("Textures/player");
    // }
    //
    // public override void Update(double delta)
    // {
    //     var dir = _actionManager.GetVector("move_left", "move_right", "move_up", "move_down");
    //     GlobalPosition += dir * Speed;
    // }
}


