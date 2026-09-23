namespace SummerGame.Core.Worlds;

public struct Tile
{
    public const int TileSize = 16;
    public TileType Type;

    // public int Hp { get; set; } = -1;
    // public float Brightness { get; set; } = 1.0f;
}

public enum TileType
{
    Air,
    Stone
}
