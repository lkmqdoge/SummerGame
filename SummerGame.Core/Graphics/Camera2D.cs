
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SummerGame.Core.Graphics;

public class Camera2D
{
    public float Zoom { get; set; } = 1;

    public Vector2 Postion { get; set; }

    public float Rotation { get; set; }

    public Matrix Transform { get; private set ;} = Matrix.Identity;

    public Matrix GetViewMatrix(Viewport viewport) =>
        Matrix.CreateTranslation(new Vector3(-Postion, 0)) *
        Matrix.CreateRotationZ(Rotation) *
        Matrix.CreateScale(new Vector3(Zoom, Zoom, 0)) *
        Matrix.CreateTranslation(viewport.Width * 0.5f, viewport.Height * 0.5f, 0);
}


