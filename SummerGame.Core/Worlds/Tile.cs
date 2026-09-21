namespace SummerGame.Core.Worlds;

public struct Tile
{
    public const int TileSize = 16;

    public TileType Type;
}

public enum TileType
{
    Air,
    Stone
}
