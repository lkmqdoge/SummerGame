using SummerGame.Core.Input;
using SummerGame.Core.Simulation.Worlds;

namespace SummerGame.Core.Simulation;

public class GameSimulation(GameCore game) : GameObject(game)
{
    public World World = new ();

    public ActionManager ActionManager = new ();

    public override void Update(double delta)
    {
        ActionManager.Update();

        World.Update(delta);
    }
}


