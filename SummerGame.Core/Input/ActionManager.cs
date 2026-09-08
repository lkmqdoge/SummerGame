using System.Collections.Generic;
using System.Numerics;

namespace SummerGame.Core.Input;

public class ActionManager
{
    public KeyBoardInfo KeyBoardInfo { get; } = new();
    public MouseInfo MouseInfo { get; } = new();

    private readonly Dictionary<string, InputAction> _actions = [];

    public void AddAction(params InputAction[] actions)
    {
        foreach (var action in actions)
            _actions.Add(action.Name, action);
    }

    public void Update()
    {
        KeyBoardInfo.Update();
        MouseInfo.Update();
    }

    public Vector2 GetVector(
        string negativeXActionName,
        string positiveXActionName,
        string negativeYActionName,
        string positiveYActionName
    )
    {
        var x = GetAxis(negativeXActionName, positiveXActionName);
        var y = GetAxis(negativeYActionName, positiveYActionName);
    
        var dir = new Vector2(x, y);
        if (dir != Vector2.Zero)
            dir = Vector2.Normalize(new Vector2(x, y));

        return dir;
    }

    public float GetAxis(string negativeDirActionName, string positiveDirActionName)
    {
        if (_actions.TryGetValue(negativeDirActionName, out var negAction)
                && _actions.TryGetValue(positiveDirActionName, out var posAction))
        {
            int p = 0;
            int n = 0;
            foreach(var key in negAction.Keys)
                if (KeyBoardInfo.IsKeyDown(key))
                    n = -1;
            foreach(var key in posAction.Keys)
                if (KeyBoardInfo.IsKeyDown(key))
                    p = 1;

            return p + n;
        }
        else
            return 0.0f;
    }
    public bool IsActionJustPressed(string actionName)
    {
        if (_actions.TryGetValue(actionName, out var action))
        {
            var c = 0;
            foreach(var key in action.Keys)
                if (KeyBoardInfo.IsKeyJustPressed(key))
                    c++;

            return c > 0;
        }
        else
            return false;
    }

    public bool IsActionPressed(string actionName)
    {
        if (_actions.TryGetValue(actionName, out var action))
        {
            var c = 0;
            foreach(var key in action.Keys)
                if (KeyBoardInfo.IsKeyDown(key))
                    c++;

            return c > 0;
        }
        else
            return false;
    }
}


