using Microsoft.Xna.Framework;

namespace SummerGame.Core.Worlds;

public readonly struct Chunk
{
    public readonly Vector2 ChunkPos { get; init; }

    public readonly Tile[, ] Tiles { get; init; }
}


