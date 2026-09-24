using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using SummerGame.Core.Simulation;
using System.Collections.Generic;
using System;
using SummerGame.Core.Graphics;
using SummerGame.Core.Entities;

namespace SummerGame.Core.Worlds;

public class World(GameCore game)
    : GameObject(game)
{
    public const int TileSize = 16;
    public const int ChunkSize = 8;

    public IChunkGenerator ChunkGenerator { get; } = new ChunkGenerator();
    // public IChunkGenerator ChunkGenerator { get; } = new FillGenerator();
    public ChunkLoader ChunkLoader { get; set; }
    public Dictionary<ChunkAdress, Chunk> LoadedChunks { get; set; } = [];
    public int SimulationRadius { get; set; } = 8;
    public Vector2 SimulationCenter { get; set; } = Vector2.Zero;
    public Camera2D Camera { get; set; } = new ();
    public Player Player { get; set; }

    private Vector2 CursorSelect = Vector2.Zero;
    private readonly TextureAtlas _tileAtlas = new ();
    private readonly Sprite2D _cursorSprite = new ();

    public override void Initialize()
    {
        Player = new (Game)
        {
            Speed = 300f
        };
        Player.Initialize();
    }

    public override void LoadContent()
    {
        _tileAtlas.Texture = Content.Load<Texture2D>("Textures/tiles");
        _tileAtlas.AddRegion("TestTile", 16*2, 0, 16, 16);

        _cursorSprite.Texture = Content.Load<Texture2D>("Textures/selection");
        Player.LoadContent();
    }

    public override void Update(double delta)
    {
        // --- Update camera
        // var dir = Game.ActionManager.GetVector("left", "right", "up", "down");
        // Camera.Position += dir * (1.0f / Camera.Zoom) * 10;
        Camera.Position = Player.Position;

        var zoomInput = Game.ActionManager.GetAxis("zoom_out", "zoom_in");
        Camera.Zoom += 0.05f * zoomInput * Camera.Zoom;

        if (Game.ActionManager.IsActionPressed("restore_camera"))
            Camera.Zoom = 1.0f;

        Camera.Zoom = Math.Max(0.1f, Camera.Zoom);
        Camera.UpdateCamera(Game.GraphicsDevice.Viewport);
        SimulationCenter = Camera.Position;

        const int ChunkPixels = ChunkSize * TileSize;
        var chunkX = (int)MathF.Floor(SimulationCenter.X / ChunkPixels);
        var chunkY = (int)MathF.Floor(SimulationCenter.Y / ChunkPixels);

        var mousePos = Game.ActionManager.MouseInfo.Position;
        CursorSelect = Camera.TranslateScreenToWorld(new Vector2(mousePos.X, mousePos.Y));

        _cursorSprite.Position = new Vector2(
            (float)Math.Floor(CursorSelect.X / TileSize) * TileSize,
            (float)Math.Floor(CursorSelect.Y / TileSize) * TileSize
        );

        Player.Update(delta);
        UpdateChunks(chunkX, chunkY, SimulationRadius);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin(
            samplerState: SamplerState.PointClamp,
            transformMatrix: Camera.Transform
        );

        // adjust every rect side to size of chunk
        var bound = Camera.VisibleArea;
        const int worldChunkSize = ChunkSize*TileSize;

        bound.X -= worldChunkSize*2;
        bound.Y -= worldChunkSize*2;
        bound.Width += worldChunkSize*4;
        bound.Height += worldChunkSize*4;

        // draw chunks
        foreach (var (chunkPos, chunk) in LoadedChunks)
        {
            var chunkCenter = new Vector2(
                (chunkPos.X * worldChunkSize) + (worldChunkSize / 2),
                (chunkPos.Y * worldChunkSize) + (worldChunkSize / 2)
            );

            if (!bound.Contains(chunkCenter))
                continue;


            for (int x = 0; x < ChunkSize; x++)
            {
                for (int y = 0; y < ChunkSize; y++)
                {
                    var tilePos = new Vector2(
                        (chunkPos.X * worldChunkSize) + (x * TileSize),
                        (chunkPos.Y * worldChunkSize) + (y * TileSize)
                    );

                    if (bound.Contains(tilePos) && chunk.Tiles[x, y].Type != TileType.Air)
                        _tileAtlas.GetRegion("TestTile").Draw(spriteBatch, tilePos, Color.White);
                }
            }

            // Debug draw
            // var chunkWorldPos = new Vector2(chunkPos.X*worldChunkSize, chunkPos.Y*worldChunkSize);
            // spriteBatch.DrawLine(chunkWorldPos, chunkWorldPos + new Vector2(worldChunkSize, 0), Color.Yellow, 3f);
            // spriteBatch.DrawLine(chunkWorldPos, chunkWorldPos + new Vector2(0, worldChunkSize), Color.Yellow, 3f);
        }

        Player.Draw(spriteBatch);

        // draw cursor
        _cursorSprite.Draw(spriteBatch);

        spriteBatch.End();
    }

    public Tile GetTileFromTilePosition(int x, int y)
    {
        var chunkPos = new ChunkAdress(
            x / ChunkSize,
            y / ChunkSize
        );

        if (LoadedChunks.TryGetValue(chunkPos, out var chunk))
        {
            return chunk.Tiles[x % ChunkSize, y % ChunkSize];
        }

        return new (){
            Type = TileType.NotGenerated
        };
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
}


