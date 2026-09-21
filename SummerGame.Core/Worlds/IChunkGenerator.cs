namespace SummerGame.Core.Worlds;

public interface IChunkGenerator
{
    int Seed { get; }

    Chunk GenerateChunk(int x, int y);
}


