using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SummerGame.Core;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    private SpriteFont _font;
    private Texture2D _texture;
    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        // var text = Content.Load<string>("test.txt");
        // Console.WriteLine(text);

        _font = Content.Load<SpriteFont>("Fonts/vonwaon");
        _texture = Content.Load<Texture2D>("Textures/tiles");

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();

        var fps = Math.Floor(1.0f / (float)gameTime.ElapsedGameTime.TotalSeconds);
        _spriteBatch.Draw(_texture, Vector2.Zero, Color.White);

        _spriteBatch.DrawString(_font, $"FPS: {fps}", new (10, 10), Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
