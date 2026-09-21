using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using SummerGame.Core.Simulation;
using System.Collections.Generic;
using System;
using SummerGame.Core.Graphics;

namespace SummerGame.Core.Worlds;

public class World(GameCore game)
    : GameObject(game)
{
    public const int TileSize = 16;
    public const int ChunkSize = 4;

    public IChunkGenerator ChunkGenerator { get; } = new ChunkGenerator();
    public ChunkLoader ChunkLoader { get; set; }
    public Dictionary<ChunkAdress, Chunk> LoadedChunks { get; set; } = [];
    public int SimulationRadius { get; set; } = 8;
    public Vector2 SimulationCenter { get; set; } = Vector2.Zero;
    public Camera2D Camera { get; set; } = new ();

    private readonly TextureAtlas _tileAtlas = new ();

    public override void LoadContent()
    {
        _tileAtlas.Texture = Content.Load<Texture2D>("Textures/tiles");
        _tileAtlas.AddRegion("TestTile", 0, 0, 16, 16);
    }

    public override void Update(double delta)
    {
        _ = delta;

        SimulationCenter = Camera.Position;
        const int ChunkPixels = ChunkSize * TileSize;
        var chunkX = (int)MathF.Floor(SimulationCenter.X / ChunkPixels);
        var chunkY = (int)MathF.Floor(SimulationCenter.Y / ChunkPixels);

        UpdateChunks(chunkX, chunkY, SimulationRadius);
        UpdateCamera();
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin(
            samplerState: SamplerState.PointClamp,
            transformMatrix: Camera.GetViewMatrix(Game.GraphicsDevice.Viewport)
        );

        var bound = Camera.GetBoundaries(Game.GraphicsDevice.Viewport);

        // draw chunks
        foreach (var (pos, chunk) in LoadedChunks)
        {
            for (int x = 0; x < ChunkSize; x++)
            {
                for (int y = 0; y < ChunkSize; y++)
                {
                    var tilePos = new Vector2(
                        (pos.X * ChunkSize * TileSize) + (x * TileSize),
                        (pos.Y * ChunkSize * TileSize) + (y * TileSize)
                    );

                    if (!bound.Contains(tilePos))
                        continue;

                    if (chunk.Tiles[x, y].Type != TileType.Air)
                        _tileAtlas.GetRegion("TestTile").Draw(spriteBatch, tilePos, Color.White);
                }
            }
        }

        spriteBatch.End();
    }

    private void UpdateChunks(int x, int y, int r)
    {
        foreach (var chunkPos in GetChunkPositionsInRadius(x, y, r))
        {
            if (LoadedChunks.TryGetValue(chunkPos, out var chunk))
            {
                // update chunk
            }
            else
            {
                LoadedChunks.Add(chunkPos, ChunkGenerator.GenerateChunk(chunkPos.X, chunkPos.Y));
            }
        }
    }

    private List<ChunkAdress> GetChunkPositionsInRadius(int x, int y, int r)
    {
        var res = new List<ChunkAdress>();
        for (int i = x - r ; i <= x + r; i++)
        {
            var rowDiff = i - x;
            var columnRange = Math.Sqrt((r*r) - (rowDiff*rowDiff));

            for (int j = (int)Math.Ceiling(y - columnRange);j <= (int)Math.Floor(y + columnRange); j++)
            {
                res.Add(new ChunkAdress(i, j));
            }
        }
        return res;
    }

    private void UpdateCamera()
    {
        var dir = Game.ActionManager.GetVector("left", "right", "up", "down");
        Camera.Position += dir * (1.0f / Camera.Zoom) * 10;

        if (Game.ActionManager.IsActionPressed("zoom_in"))
            Camera.Zoom += 0.03f;

        if (Game.ActionManager.IsActionPressed("zoom_out"))
            Camera.Zoom -= 0.03f;

        if (Game.ActionManager.IsActionPressed("restore_camera"))
            Camera.Zoom = 1.0f;

        Camera.Zoom = Math.Max(0.1f, Camera.Zoom);
    }
}


