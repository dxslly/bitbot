using BitBots.BitBomber.Features.Synchronized;
using Entitas;

namespace BitBots.BitBomber.Features.Tiles
{
    [Core]
    [IsSyncData]
    public class TilePositionComponent : IComponent
    {
        public int x;
        public int y;
    }
}