using System.Collections;
using Entitas;


namespace BitBots.BitBomber.Features.Synchronized
{
    public class IsSyncDataAttribute : System.Attribute
    {

    }

    [CoreAttribute]
    public class SynchronizedComponent : IComponent
    {
        public int id;
        public static int NextId = 0; // TODO Make game dependent so running continously is not a problem
    }
}