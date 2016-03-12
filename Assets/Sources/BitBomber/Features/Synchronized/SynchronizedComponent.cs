using System.Collections;
using Entitas;


namespace BitBots.BitBomber.Features.Synchronized
{
    public class IsSyncDataAttribute : System.Attribute
    {

    }

    public enum EntityType { None, Player, Bomb};
    [CoreAttribute]
    public class SynchronizedComponent : IComponent
    {
        public int id;
        public EntityType Type;
        public static int NextId = 0; // TODO Make game dependent so running continously is not a problem
    }
}