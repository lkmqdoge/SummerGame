namespace SummerGame.Core.Worlds;

public class FillGenerator
    : IChunkGenerator
{
    public int Seed { get; set; }

    public Chunk GenerateChunk(int x, int y)
    {
        var chunk = new Chunk
        {
            Tiles = new Tile[World.ChunkSize, World.ChunkSize]
        };
        for (int i = 0; i < World.ChunkSize; i++)
        {
            for (int j = 0; j < World.ChunkSize; j++)
            {
                chunk.Tiles[i, j] = new Tile(){
                    Type = TileType.Stone
                };
            }
        }

        return chunk;
    }
}


