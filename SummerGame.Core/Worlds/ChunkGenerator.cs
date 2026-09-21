using System;

namespace SummerGame.Core.Worlds;

public class ChunkGenerator
{
    public int Seed;

    public FastNoiseLite Noise = new ();

    public ChunkGenerator(int? seed = null)
    {
        Seed = seed ?? new Random().Next();

        Noise = new ();
        Noise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2);
    }

    public Chunk Generate(int x, int y)
    {
        const int chunkSize = World.ChunkSize;
        var tiles = new Tile[chunkSize, chunkSize];

        for (int i = 0; i < chunkSize; i++)
        {
            for (int j = 0; j < chunkSize; j++)
            {
                var noiseValue = Noise.GetNoise(
                    (x * chunkSize) + i,
                    (y * chunkSize) + j
                );
                if (noiseValue > 0)
                {
                    tiles[i, j] = new Tile(){
                        Type = TileType.Stone
                    };

                }
            }
        }

        return new Chunk(){
            Tiles = tiles
        };
    }
}


