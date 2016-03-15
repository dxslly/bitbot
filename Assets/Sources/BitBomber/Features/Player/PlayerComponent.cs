using Entitas;
using Entitas.CodeGenerator;

namespace BitBots.BitBomber.Features.Player
{
    [CoreAttribute]
    [BitBots.BitBomber.Features.Synchronized.IsSyncData]
    public class PlayerComponent : IComponent
    {
        public int playerID;
    }
}