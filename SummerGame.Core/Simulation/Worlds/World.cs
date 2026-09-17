using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SummerGame.Core.Simulation.Worlds;

public class World(GameSimulation simulation)
{
    // Constants
    public const int TileSize = 16;
    public const int ChunkSize = 4;

    public ChunkGenerator ChunkGenerator { get; set; }
    public ChunkLoader ChunkLoader { get; set; }

    public GameSimulation Simulation { get; set; } = simulation;
    public Dictionary<Vector2, Chunk> LoadedChunks { get; set; } = [];
    public int SimulationRadius { get; set; } = 4;
    public Vector2 SimulationCenter { get; set; } = Vector2.Zero;

    public Camera2D Camera { get; set; } = new ();

    public void Update(double delta)
    {
        _ = delta;
        LoadedChunks = UpdateChunks(SimulationCenter, SimulationRadius, LoadedChunks);
        UpdateCamera();
    }

    private Dictionary<Vector2, Chunk> UpdateChunks(Vector2 center,
        int radius, Dictionary<Vector2, Chunk> loadedChunks)
    {
        center = Vector2.Floor(center);
        var res = new Dictionary<Vector2, Chunk>();
        foreach (var chunkPos in GetChunkPositionsInRadius((int)center.X, (int)center.Y, radius))
        {
            if (loadedChunks.TryGetValue(chunkPos, out var chunk))
            {
                res.Add(chunkPos, chunk);
            }
            // else if (ChunkLoader.TryLoadChunk(out var loadedChunk))
            // {
            //     res.Add(chunkPosition, loadedChunk);
            // }
            else
            {
                res.Add(chunkPos, new Chunk());
                // res.Add(chunkPosition, ChunkGenerator.Generate(
                //     (int)chunkPosition.X,
                //     (int)chunkPosition.Y
                // ));
            }
        }

        return res;
    }

    private List<Vector2> GetChunkPositionsInRadius(int x, int y, int r)
    {
        var res = new List<Vector2>();
        for (int i = x - r ; i < x + r; i++)
        {
            var rowDiff = i - x;
            var columnRange = Math.Sqrt((r*r) - (rowDiff*rowDiff));

            for (int j = (int)Math.Ceiling(y - columnRange);j < (int)Math.Floor(y + columnRange); j++)
            {
                res.Add(new Vector2(i, j));
            }
        }
        return res;
    }

    private void UpdateCamera()
    {
        var dir = Simulation.ActionManager.GetVector("left", "right", "up", "down");
        Camera.Postion += dir * (1.0f / Camera.Zoom) * 10;

        if (Simulation.ActionManager.IsActionPressed("zoom_in"))
            Camera.Zoom += 0.03f;

        if (Simulation.ActionManager.IsActionPressed("zoom_out"))
            Camera.Zoom -= 0.03f;

        Camera.Zoom = Math.Max(0.1f, Camera.Zoom);
    }
}


