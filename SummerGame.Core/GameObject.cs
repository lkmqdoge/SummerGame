using System;
using Microsoft.Xna.Framework.Content;

namespace SummerGame.Core;

public abstract class GameObject :
    IGameObject, IDisposable
{
    public bool IsDisposed { get; private set; }

    protected ContentManager Content;

    protected GameObject(GameCore game)
    {
        Content = new ContentManager(game.Content.ServiceProvider)
        {
            RootDirectory = game.Content.RootDirectory
        };
    }

    ~GameObject() => Dispose(false);

    public virtual void Initialize() { }

    public virtual void LoadContent() { }

    public virtual void Update(double delta) { }

    public virtual void Exit()
    {
        Content.Unload();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (IsDisposed)
            return;

        if (disposing)
        {
            Exit();
            Content.Dispose();
        }
        IsDisposed = true;
    }
}


