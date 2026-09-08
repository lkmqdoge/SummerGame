using Microsoft.Xna.Framework.Graphics;
using SummerGame.Core.Simulation;

namespace SummerGame.Core.Graphics;

public class GameGraphics(GameCore game)
    : GameObject(game), IDrawable
{
    public bool Visible { get; set; }

    public GameSimulation Simulation = game.Simulation;

    public void Draw(SpriteBatch spriteBatch)
    {
        if (!Visible)
            return;

        // draw chunks
        var chunks = Simulation.World.LoadedChunks;
        // foreach (var (pos, chunk) in chunks)
        // {
        //     spriteBatch.Draw();
        // }
    }
}


