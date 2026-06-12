using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SummerGame.Core.Graphics;
using SummerGame.Core.Input;
using SummerGame.Core.Simulation;
using SummerGame.Core.UI;

namespace SummerGame.Core;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Grid _grid;
    private Camera2D _camera = new();

    private TileGridDisplay _tileGridDisplay;

    private KeyBoardInfo _keyboardInfo = new();

    private DebugPanel _debugPanel;

    public Game1()
    {
        _graphics = new (this);

        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _tileGridDisplay = new();
        _grid = new (100, 100);

        _tileGridDisplay.Grid = _grid;
        _tileGridDisplay.HardcodedShit = new();

        for (int x = 0; x < _grid.Tiles.GetLength(0); x++)
        {
            for (int y = 0; y < _grid.Tiles.GetLength(1); y++)
            {
                _grid.Tiles[x, y] = new Tile{
                    BlockId = "debug-purple",
                };
            }
        }

        base.Initialize();
    }

    private SpriteFont _font;
    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        // var text = Content.Load<string>("test.txt");
        // Console.WriteLine(text);

        // just like loading from file
        _tileGridDisplay.HardcodedShit.Texture = Content.Load<Texture2D>("Textures/tiles");
        _tileGridDisplay.HardcodedShit.AddRegion("debug-purple", 2*16, 0, 16, 16);

        _font = Content.Load<SpriteFont>("Fonts/NotJamMonoClean16");
        _debugPanel = new (_font, _spriteBatch, this);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _keyboardInfo.Update();

        // camera controls
        if (_keyboardInfo.IsKeyDown(Keys.OemPlus))  _camera.Zoom += 0.05f;
        if (_keyboardInfo.IsKeyDown(Keys.OemMinus)) _camera.Zoom -= 0.05f;

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // world step
        _spriteBatch.Begin(
            samplerState: SamplerState.PointClamp,
            transformMatrix: _camera.GetViewMatrix(GraphicsDevice.Viewport)
        );

        _tileGridDisplay.Draw(_spriteBatch);

        _spriteBatch.End();

        // ui step
        _spriteBatch.Begin();

        _debugPanel.Draw(gameTime);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
