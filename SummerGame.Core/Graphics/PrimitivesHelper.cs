using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SummerGame.Core.Graphics;

public static class PrimitivesHelper
{
    private static Texture2D _pixel;

    private static void CreatePixel(GraphicsDevice graphicsDevice)
    {
        _pixel = new Texture2D(graphicsDevice, 1, 1, false, SurfaceFormat.Color);
        _pixel.SetData([Color.White]);
    }

    public static void DrawLine(this SpriteBatch spriteBatch, Vector2 p1, Vector2 p2, Color color)
    {
        DrawLine(spriteBatch, p1, p2, color, 1.0f);
    }

    public static void DrawLine(this SpriteBatch spriteBatch, Vector2 p1, Vector2 p2, Color color, float thickness)
    {
        var distance = Vector2.Distance(p1, p2);
        var angle = (float)Math.Atan2(p2.Y - p1.Y, p2.X - p2.X);

        DrawLine(spriteBatch, p1, distance, angle, color, thickness);
    }

    public static void DrawLine(this SpriteBatch spriteBatch, Vector2 p1, float length, float angle, Color color, float thickness)
		{
			if (_pixel == null)
			{
				CreatePixel(spriteBatch.GraphicsDevice);
			}

			spriteBatch.Draw(_pixel,
                p1,
                null,
                color,
                angle,
                Vector2.Zero,
                new Vector2(length, thickness),
                SpriteEffects.None,
                0
            );
		}
}


