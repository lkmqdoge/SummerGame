using Microsoft.Xna.Framework.Input;
using SummerGame.Core.Input;
using SummerGame.Core.Simulation.Worlds;

namespace SummerGame.Core.Simulation;

public class GameSimulation : GameObject
{
    public World World { get; set; }

    public ActionManager ActionManager = new ();

    public GameSimulation(GameCore game) : base(game)
    {
        World = new (this);

        ActionManager.AddAction([
            new InputAction("left", [Keys.Left]),
            new InputAction("right", [Keys.Right]),
            new InputAction("up", [Keys.Up]),
            new InputAction("down", [Keys.Down]),

            new InputAction("zoom_in",  [Keys.OemPlus]),
            new InputAction("zoom_out", [Keys.OemMinus])
        ]);
    }

    public override void Update(double delta)
    {
        ActionManager.Update();
        World.Update(delta);
    }
}


