using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerGame.Core.Simulation;

namespace SummerGame.Core.Graphics;
public class TileGridDisplay
{
    public TextureAtlas HardcodedShit { get; set; }

    public Grid Grid { get; set; }

    public int TileSize { get; set; } = 16;

    public TileGridDisplay()
    {
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        for (int x = 0; x < Grid.Tiles.GetLength(0); x++)
        {
            for (int y = 0; y < Grid.Tiles.GetLength(1); y++)
            {
                var id = Grid.Tiles[x, y].BlockId;
                var reg = HardcodedShit.GetRegion(id);
                var pos = new Vector2(x, y) * TileSize;

                reg.Draw(spriteBatch, pos, Color.White);
            }
        }
    }
}



