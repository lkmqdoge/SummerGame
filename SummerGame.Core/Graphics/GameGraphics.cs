using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerGame.Core.Simulation;
using SummerGame.Core.Simulation.Worlds;

namespace SummerGame.Core.Graphics;

public class GameGraphics : GameObject, IDrawable
{
    public bool Visible { get; set; } = true;
    public GameSimulation Simulation { get; set; }

    private TextureAtlas _tileAtlas = new ();

    public GameGraphics(GameCore game) : base(game)
    {
        Simulation = game.Simulation;
    }

    public override void LoadContent()
    {
        _tileAtlas.Texture = Content.Load<Texture2D>("Textures/tiles");
        _tileAtlas.AddRegion("TestTile", 0, 0, 16, 16);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (!Visible)
            return;

        spriteBatch.Begin(
            samplerState: SamplerState.PointClamp,
            transformMatrix: Simulation.World.Camera.GetViewMatrix(Game.GraphicsDevice.Viewport)
        );

        // draw chunks
        var chunks = Simulation.World.LoadedChunks;
        foreach (var (pos, chunk) in chunks)
        {
            for (int x = 0; x < Tile.TileSize; x++)
            {
                for (int y = 0; y < Tile.TileSize; y++)
                {
                    var tilePos = (pos * Chunk.ChunkSize * Tile.TileSize)
                        + (new Vector2(x, y) * Tile.TileSize);

                    _tileAtlas.GetRegion("TestTile").Draw(spriteBatch, tilePos, Color.White);
                }
            }
        }

        spriteBatch.End();
    }
}


