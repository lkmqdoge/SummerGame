using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SummerGame.Core.UI;

public class DebugPanel(GameCore game)
    : GameObject(game)
{
    public bool Visible { get; set; } = true;

    private SpriteFont _font;
    private readonly string _format = """
        FPS:       {0}
        DrawCalls: {1}
        """;

    public override void LoadContent()
    {
        _font = Content.Load<SpriteFont>("Fonts/NotJamMonoClean16");
    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        if (!Visible)
        {
            return;
        }

        var fps = Math.Floor(1.0f / (float)gameTime.ElapsedGameTime.TotalSeconds);
        var drawCalls = Game.GraphicsDevice.Metrics.DrawCount;

        // ui step
        spriteBatch.Begin();
        spriteBatch.DrawString(_font, string.Format(_format, fps, drawCalls), new (10, 10), Color.White);
        spriteBatch.End();
    }
}


