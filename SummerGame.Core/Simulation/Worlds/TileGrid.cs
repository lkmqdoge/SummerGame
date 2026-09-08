using Microsoft.Xna.Framework;

namespace SummerGame.Core.Simulation.Worlds;

public class TileGrid
{
    public Tile[,] Tiles;

    public Vector2 Origin { get; set; } = Vector2.Zero;

    public TileGrid(Tile[,] tiles)
    {
        Tiles = tiles;
    }

    public TileGrid(int w, int h)
    {
        Tiles = new Tile[w, h];
    }
}


