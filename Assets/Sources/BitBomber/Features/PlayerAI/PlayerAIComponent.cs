using Entitas;
using Entitas.CodeGenerator;

namespace BitBots.BitBomber.Features.PlayerAI
{
    [CoreAttribute]
    public class PlayerAIComponent : IComponent
    {
        public IPlayerAI playerID;
    }
}