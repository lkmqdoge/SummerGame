using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerGame.Core.Simulation;

namespace SummerGame.Core.Graphics;

public class GameGraphics(GameCore game)
    : GameObject(game), IDrawable
{
    public bool Visible { get; set; }

    public GameSimulation Simulation { get; set; } = game.Simulation;

    private TextureAtlas _tileAtlas = new ();

    public override void LoadContent()
    {
        _tileAtlas.Texture = Content.Load<Texture2D>("Textures/tiles");
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (!Visible)
            return;

        // draw chunks
        var chunks = Simulation.World.LoadedChunks;
        foreach (var (pos, chunk) in chunks)
        {
            for (int x = 0; x < Tile.TileSize; x++)
            {
                for (int y = 0; y < Tile.TileSize; y++)
                {
                    var tilePos = (pos * Chunk.ChunkSize * Tile.TileSize)
                        + new Vector2(x, y);

                    spriteBatch.Draw();
                }
            }
        }
    }
}


