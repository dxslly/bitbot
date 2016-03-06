using Entitas;
using UnityEngine;

namespace BitBots.BitBomber.Features.Tiles
{
    public class RenderTilePositionSystem : IReactiveSystem, IEnsureComponents
    {
        public TriggerOnEvent trigger
        {
            get { return Matcher.AllOf(CoreMatcher.TilePosition, CoreMatcher.View).OnEntityAdded(); }
        }
        
        public IMatcher ensureComponents { get { return  Matcher.AllOf(CoreMatcher.TilePosition, CoreMatcher.View); }}

        public void Execute(System.Collections.Generic.List<Entity> entities)
        {
            foreach (var e in entities)
            {
                e.view.gameObject.transform.position = new Vector2(e.tilePosition.x, e.tilePosition.y);
            }
        }
    }
}
