using Entitas;

namespace BitBots.BitBomber.Features.Expireable
{
    [Core]
    public class ExpireableComponent : IComponent
    {
        public int remainingTicksToLive;
    }
}