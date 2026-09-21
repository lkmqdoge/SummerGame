using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SummerGame.Core.Simulation;

public class Camera2D
{
    public float Zoom { get; set; } = 1;

    public Vector2 Position { get; set; }

    public float Rotation { get; set; }

    public Matrix Transform { get; private set ;} = Matrix.Identity;

    public Matrix GetViewMatrix(Viewport viewport)
    {
        Transform = Matrix.CreateTranslation(new Vector3(-Position, 0)) *
            Matrix.CreateRotationZ(Rotation) *
            Matrix.CreateScale(new Vector3(Zoom, Zoom, 0)) *
            Matrix.CreateTranslation(viewport.Width * 0.5f, viewport.Height * 0.5f, 0);
        return Transform;
    }

    public Rectangle GetBoundaries(Viewport viewport)
    {
        var inverseTransform =  Matrix.Invert(Transform);
        var cameraTopLeft = Position - new Vector2(viewport.Width / 2f, viewport.Height / 2f);
        var cameraBottomRight = Position + new Vector2(viewport.Width / 2f, viewport.Height / 2f);

        var cameraTopLeftWorld = Vector2.Transform(cameraTopLeft, inverseTransform);
        var cameraBottomRightWorld = Vector2.Transform(cameraBottomRight, inverseTransform);

        var width = cameraBottomRightWorld.X - cameraTopLeftWorld.X;
        var height = cameraBottomRightWorld.Y - cameraTopLeftWorld.Y;

        var bounds = new Rectangle(
            (int)cameraTopLeftWorld.X,
            (int)cameraTopLeftWorld.Y,
            (int)width,
            (int)height
        );
        return bounds;
    }
}


