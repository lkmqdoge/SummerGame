using Microsoft.Xna.Framework;

namespace SummerGame.Core.Simulation.Worlds;

public readonly struct Chunk
{
    public static int ChunkSize { get; set; } = 16;

    public readonly Vector2 ChunkPos { get; init; }

    public readonly Tile[, ] Tiles { get; init; }
}


