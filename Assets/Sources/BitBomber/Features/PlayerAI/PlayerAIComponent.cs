using Entitas;
using Jint;

namespace BitBots.BitBomber.Features.PlayerAI
{
    [CoreAttribute]
    public class PlayerAIComponent : IComponent
    {
        public JintEngine engine;
    }
}