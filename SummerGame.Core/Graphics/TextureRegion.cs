using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SummerGame.Core.Graphics;

public class TextureRegion
{
    public Texture2D Texture         { get; set; }
    public Rectangle SourceRectangle { get; set; }

    public int Width  => SourceRectangle.Width;
    public int Height => SourceRectangle.Height;

    public TextureRegion() {}

    public TextureRegion(Texture2D texture, int x, int y, int w, int h)
    {
        Texture = texture;
        SourceRectangle = new (x, y, w, h);
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color)
        => Draw(
                spriteBatch,
                position,
                color,
                0.0f,
                Vector2.Zero,
                Vector2.One,
                SpriteEffects.None,
                0.0f
               );

    public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color, float rotation,
            Vector2 origin, float scale, SpriteEffects effects, float layerDepth)
        => Draw(
                spriteBatch,
                position,
                color,
                rotation,
                origin,
                new Vector2(scale, scale),
                effects,
                layerDepth
               );

    public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color, float rotation,
            Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
        => spriteBatch.Draw(
                Texture,
                position,
                SourceRectangle,
                color,
                rotation,
                origin,
                scale,
                effects,
                layerDepth
                );
}
