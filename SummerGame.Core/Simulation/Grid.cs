namespace SummerGame.Core.Simulation;

public class Grid
{
    public Tile[,] Tiles;

    public Grid(Tile[,] tiles)
    {
        Tiles = tiles;
    }

    public Grid(int w, int h)
    {
        Tiles = new Tile[w, h];
    }
}

public readonly struct Tile
{
    public readonly string BlockId { get; init; }
}

