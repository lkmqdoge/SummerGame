using Microsoft.Xna.Framework;

namespace SummerGame.Core.Simulation.Worlds;

public readonly struct Chunk
{
    public readonly Vector2 ChunkPos { get; init; }

    public readonly Tile[, ] Tiles { get; init; }
}


