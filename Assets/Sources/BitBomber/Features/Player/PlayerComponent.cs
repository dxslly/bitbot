using Entitas;
using Entitas.CodeGenerator;

namespace BitBots.BitBomber.Features.Player
{
    [CoreAttribute]
    public class PlayerComponent : IComponent
    {
        public string playerID;
    }
}