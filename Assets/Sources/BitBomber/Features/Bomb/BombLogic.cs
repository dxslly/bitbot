using Entitas;
using BitBots.BitBomber.Features.Owner;

namespace BitBots.BitBomber.Features.Bomb
{
    public class BombLogic
    {
        public bool CanPlaceBomb(OwnerComponent owner, int x, int y)
        {
            Group bombs = Pools.core.GetGroup(Matcher.AllOf(CoreMatcher.Bomb, CoreMatcher.Owner));
            foreach (var e in bombs.GetEntities())
            {
                if (e.hasOwner)
                {
                    return e.owner.Equals(owner);
                }
            }
            
            return true;
        }
    }
}