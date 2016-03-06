using Entitas;
using BitBots.BitBomber.Features.Player;

namespace BitBots.BitBomber.Features.Bomb
{
    [Core]
    public class BombComponent : IComponent
    {
        public int remainingFuseTime;
    }
}