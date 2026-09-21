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
    // Constants
    public const int TileSize = 16;
    public const int ChunkSize = 8;

    public ChunkGenerator ChunkGenerator { get; } = new ();
    public ChunkLoader ChunkLoader { get; set; }
    public Dictionary<Vector2, Chunk> LoadedChunks { get; set; } = [];
    public int SimulationRadius { get; set; } = 12;
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

        var center = Vector2.Floor(SimulationCenter);

        foreach (var chunkPos in GetChunkPositionsInRadius((int)center.X, (int)center.Y, SimulationRadius))
        {
            if (!LoadedChunks.ContainsKey(chunkPos))
            {
                LoadedChunks.Add(chunkPos, ChunkGenerator.Generate(
                    (int)chunkPos.X,
                    (int)chunkPos.Y
                ));
            }
        }

        UpdateCamera();
        SimulationCenter = Camera.Postion;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin(
            samplerState: SamplerState.PointClamp,
            transformMatrix: Camera.GetViewMatrix(Game.GraphicsDevice.Viewport)
        );

        // draw chunks
        var chunks = LoadedChunks;
        foreach (var (pos, chunk) in chunks)
        {
            for (int x = 0; x < ChunkSize; x++)
            {
                for (int y = 0; y < ChunkSize; y++)
                {
                    var tilePos = (pos * ChunkSize * Tile.TileSize)
                        + (new Vector2(x, y) * Tile.TileSize);

                    if (chunk.Tiles[x, y].Type != TileType.Air)
                        _tileAtlas.GetRegion("TestTile").Draw(spriteBatch, tilePos, Color.White);
                }
            }
        }

        spriteBatch.End();
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
        var dir = Game.ActionManager.GetVector("left", "right", "up", "down");
        Camera.Postion += dir * (1.0f / Camera.Zoom) * 10;

        if (Game.ActionManager.IsActionPressed("zoom_in"))
            Camera.Zoom += 0.03f;

        if (Game.ActionManager.IsActionPressed("zoom_out"))
            Camera.Zoom -= 0.03f;

        Camera.Zoom = Math.Max(0.1f, Camera.Zoom);
    }
}


