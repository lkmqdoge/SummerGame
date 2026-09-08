using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SummerGame.Core.Simulation.Worlds;

public class World
{
    // Constants
    public const int TileSize = 16;
    public const int ChunkSize = 16;

    public ChunkGenerator ChunkGenerator { get; set; }
    public ChunkLoader ChunkLoader { get; set; }


    public Dictionary<Vector2, Chunk> LoadedChunks { get; set; }
    public int SimulationRadius { get; set; } = 10;
    public Vector2 SimulationCenter { get; set; } = Vector2.Zero;

    public Camera2D Camera { get; set; }

    public void Update(double delta)
    {
        _ = delta;
        LoadedChunks = UpdateChunks(SimulationCenter, SimulationRadius, LoadedChunks);
    }

    public Dictionary<Vector2, Chunk> UpdateChunks(Vector2 center,
        int radius, Dictionary<Vector2, Chunk> loadedChunks)
    {
        center = Vector2.Floor(center);
        var res = new Dictionary<Vector2, Chunk>();

        for (int r = 0; r < radius; r++)
        {
            for (int i = r; i >= -r; i--)
            {
                // load chunk center + (+-(r-abs(i)), i)
                var chunkPosition = center + new Vector2(r - Math.Abs(i), i);
                if (loadedChunks.TryGetValue(chunkPosition, out var chunk))
                {
                    res.Add(chunkPosition, chunk);
                }
                // else if (ChunkLoader.TryLoadChunk(out var loadedChunk))
                // {
                //     res.Add(chunkPosition, loadedChunk);
                // }
                else
                {
                    res.Add(chunkPosition, new Chunk());
                    // res.Add(chunkPosition, ChunkGenerator.Generate(
                    //     (int)chunkPosition.X,
                    //     (int)chunkPosition.Y
                    // ));
                }
            }
        }

        return res;
    }
}


