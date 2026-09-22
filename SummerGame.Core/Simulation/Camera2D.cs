using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SummerGame.Core.Simulation;

public class Camera2D()
{
    public float Zoom { get; set; } = 1f;
    public float Rotation { get; set; }
    public Vector2 Position { get; set; } = Vector2.Zero;
    public Matrix Transform { get; private set; } = Matrix.Identity;
    public Matrix InverseTransform { get; private set; } = Matrix.Identity;
    public Rectangle VisibleArea { get; private set; }

    public void UpdateCamera(Viewport viewport)
    {
        Transform = Matrix.CreateTranslation(new Vector3(-Position, 0)) *
            Matrix.CreateRotationZ(Rotation) *
            Matrix.CreateScale(Zoom) *
            Matrix.CreateTranslation(new Vector3(viewport.Width * 0.5f, viewport.Height * 0.5f, 0));

        InverseTransform = Matrix.Invert(Transform);
        VisibleArea = UpdateVisibleArea(viewport);
    }

    private Rectangle UpdateVisibleArea(Viewport viewport)
    {
        var x = Position.X - (viewport.Width  / 2f / Zoom);
        var y = Position.Y - (viewport.Height / 2f / Zoom);
        var width = viewport.Width / Zoom;
        var height = viewport.Height / Zoom;

        return new Rectangle(
            (int)x,
            (int)y,
            (int)width,
            (int)height
        );
    }
}


