using Microsoft.Xna.Framework.Graphics;

namespace SummerGame.Core.Graphics;

public interface IDrawable
{
    bool Visible { get; set; }
    void Draw(SpriteBatch spriteBatch);
}


