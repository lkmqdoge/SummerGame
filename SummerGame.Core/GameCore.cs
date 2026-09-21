using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SummerGame.Core.Graphics;
using SummerGame.Core.Input;
using SummerGame.Core.Simulation;
using SummerGame.Core.UI;
using SummerGame.Core.Worlds;

namespace SummerGame.Core;

public class GameCore : Game
{
    public ActionManager ActionManager { get; } = new ();

    private GraphicsDeviceManager _graphicsDevice;
    private SpriteBatch _spriteBatch;

    private World _world;

    public GameCore()
    {
        _graphicsDevice = new (this);

        ActionManager.AddAction([
            new InputAction("left", [Keys.Left]),
            new InputAction("right", [Keys.Right]),
            new InputAction("up", [Keys.Up]),
            new InputAction("down", [Keys.Down]),

            new InputAction("zoom_in",  [Keys.OemPlus]),
            new InputAction("zoom_out", [Keys.OemMinus])
        ]);


        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        _world = new (this);
    }

    protected override void Initialize()
    {
        _world.Initialize();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _world.LoadContent();

        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        var delta = gameTime.ElapsedGameTime.TotalSeconds;

        ActionManager.Update();
        _world.Update(delta);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _world.Draw(_spriteBatch);

        base.Draw(gameTime);
    }
}
