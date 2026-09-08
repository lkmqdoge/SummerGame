using Microsoft.Xna.Framework.Input;

namespace SummerGame.Core.Input;

public readonly struct InputAction(string name, Keys[] keys)
{
    public readonly Keys[] Keys { get; } = keys;
    public readonly string Name { get; } = name;
}
