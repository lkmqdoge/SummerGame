using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace SummerGame.Core.Graphics;

public class TextureAtlas(Texture2D texture = null)
{
    public Texture2D Texture { get; set; } = texture;

    private readonly Dictionary<string, TextureRegion> _regions = [];

    public void AddRegion(string name, int x, int y, int w, int h)
    {
        var region = new TextureRegion(Texture, x, y, w, h);
        _regions.Add(name, region);
    }

    public TextureRegion GetRegion(string name)
        => _regions[name];

    public bool RemoveRegion(string name)
        => _regions.Remove(name);

    public void Clear()
        => _regions.Clear();
}
