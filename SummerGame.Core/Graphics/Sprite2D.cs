using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SummerGame.Core.Graphics;

public class Sprite2D(Texture2D texture = null)
{
    public Texture2D Texture { get; set; } = texture;

    public Vector2 Position { get; set; } = Vector2.Zero;

    public Color Color { get; set; } = Color.White;

    public float Rotation { get; set; }

    public Vector2 Origin { get; set; } = Vector2.Zero;

    public float Scale { get; set; } = 1.0f;

    public SpriteEffects Effect { get; set; } = SpriteEffects.None;

    public float LayerDepth { get; set; }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
                Texture,
                Position,
                null,
                Color,
                Rotation,
                Origin,
                Scale,
                Effect,
                LayerDepth
                );
    }
}

