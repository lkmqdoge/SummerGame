using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SummerGame.Core.UI;

public class DebugPanel(SpriteFont font, SpriteBatch spriteBatch, Game game)
    : IGameDrawable
{
    public bool Visible { get; set; } = true;
    private readonly string _format = """
        FPS:       {0}
        DrawCalls: {1}
        """;

    public void Draw(GameTime gameTime)
    {
        if (!Visible)
        {
            return;
        }

        var fps = Math.Floor(1.0f / (float)gameTime.ElapsedGameTime.TotalSeconds);
        var drawCalls = game.GraphicsDevice.Metrics.DrawCount;

        // ui step
        spriteBatch.DrawString(font, string.Format(_format, fps, drawCalls), new (10, 10), Color.White);
    }
}


