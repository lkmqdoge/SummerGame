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
    private DebugPanel _debugPanel;

    private World _world;

    public GameCore()
    {
        _graphicsDevice = new (this);

        ActionManager.AddAction([
            new InputAction("left", [Keys.Left]),
            new InputAction("right", [Keys.Right]),
            new InputAction("up", [Keys.Up]),
            new InputAction("down", [Keys.Down]),

            new InputAction("move_left", [Keys.A]),
            new InputAction("move_right", [Keys.D]),
            new InputAction("move_up", [Keys.W]),
            new InputAction("move_down", [Keys.S]),

            new InputAction("zoom_in",  [Keys.OemPlus]),
            new InputAction("zoom_out", [Keys.OemMinus]),
            new InputAction("restore_camera", [Keys.R])
        ]);


        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        _world = new (this);
        _debugPanel = new (this);
    }

    protected override void Initialize()
    {
        _world.Initialize();
        _debugPanel.Initialize();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _world.LoadContent();
        _debugPanel.LoadContent();

        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        var delta = gameTime.ElapsedGameTime.TotalSeconds;

        ActionManager.Update();
        _world.Update(delta);
        _debugPanel.Update(delta);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _world.Draw(_spriteBatch);
        _debugPanel.Draw(_spriteBatch, gameTime);

        base.Draw(gameTime);
    }
}
