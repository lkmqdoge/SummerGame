using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SummerGame.Core.Input;
using SummerGame.Core.UI;
using SummerGame.Core.Worlds;

namespace SummerGame.Core;

public class GameCore : Game
{
    public ActionManager ActionManager { get; } = new ();

    private readonly GraphicsDeviceManager _graphicsDevice;
    private SpriteBatch _spriteBatch;
    private readonly DebugPanel _debugPanel;
    private readonly GameObject _rootGameObject;

    public GameCore()
    {
        _graphicsDevice = new (this);
        Window.AllowUserResizing = true;

        ActionManager.AddAction([
            new InputAction("left", [Keys.Left]),
            new InputAction("right", [Keys.Right]),
            new InputAction("up", [Keys.Up]),
            new InputAction("down", [Keys.Down]),

            new InputAction("move_left", [Keys.A]),
            new InputAction("move_right", [Keys.D]),
            new InputAction("move_up", [Keys.W]),
            new InputAction("move_down", [Keys.S]),
            new InputAction("sprint", [Keys.LeftShift]),

            new InputAction("zoom_in",  [Keys.OemPlus]),
            new InputAction("zoom_out", [Keys.OemMinus]),
            new InputAction("restore_camera", [Keys.R])
        ]);


        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        _rootGameObject = new World(this);
        _debugPanel = new (this);
    }

    protected override void Initialize()
    {
        _rootGameObject.Initialize();
        _debugPanel.Initialize();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _rootGameObject.LoadContent();
        _debugPanel.LoadContent();

        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        var delta = gameTime.ElapsedGameTime.TotalSeconds;

        ActionManager.Update();
        _rootGameObject.Update(delta);
        _debugPanel.Update(delta);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _rootGameObject.Draw(_spriteBatch);
        _debugPanel.Draw(_spriteBatch, gameTime);

        base.Draw(gameTime);
    }
}
