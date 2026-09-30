using System.Collections.Generic;

namespace SummerGame.Core.Physics;

public interface IPhysics
{
    List<ICollisionBody> Bodies { get; }
    List<ICollisionArea> Areas { get; }
}


