using System;

namespace SummerGame.Core.Simulation.Worlds;

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
        // var tiles = new Tile[Chunk.ChunkSize, Chunk.ChunkSize];
        //
        // for (int i = 0; i < Chunk.ChunkSize; i++)
        // {
        //     for (int j = 0; j < Chunk.ChunkSize; j++)
        //     {
        //         var noiseValue = Noise.GetNoise(
        //             (x * Chunk.ChunkSize) + i,
        //             (y * Chunk.ChunkSize) + i
        //         );
        //         // tiles[i, j] = new (){
        //         //     BlockId = noiseValue
        //         // };
        //     }
        // }
        //
        return new Chunk(){
        };
    }
}


