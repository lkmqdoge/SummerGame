using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerGame.Core.Graphics;
using SummerGame.Core.Simulation;

namespace SummerGame.Core;

public class GameCore : Game
{
    public GameSimulation Simulation { get; set; }
    public GameGraphics Graphics { get; set; }

    private GraphicsDeviceManager _graphicsDevice;
    private SpriteBatch _spriteBatch;

    public GameCore()
    {
        _graphicsDevice = new (this);

        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        Simulation = new (this);
        Graphics = new (this);
    }

    protected override void Initialize()
    {
        Simulation.Initialize();
        Graphics.Initialize();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        Simulation.LoadContent();
        Graphics.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        var delta = gameTime.ElapsedGameTime.TotalSeconds;

        Simulation.Update(delta);
        Graphics.Update(delta);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        Graphics.Draw(_spriteBatch);

        base.Draw(gameTime);
    }
}
