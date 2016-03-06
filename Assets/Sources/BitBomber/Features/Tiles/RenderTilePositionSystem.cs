using Entitas;
using System;
using UnityEngine;

namespace BitBots.BitBomber.Features.Tiles
{
    public class RenderTilePositionSystem : IReactiveSystem
    {
        public TriggerOnEvent trigger
        {
            get { return Matcher.AllOf(CoreMatcher.TilePosition, CoreMatcher.View).OnEntityAdded(); }
        }

        public void Execute(System.Collections.Generic.List<Entity> entities)
        {
            foreach (var e in entities)
            {
                e.view.gameObject.transform.position = new Vector2(e.tilePosition.x, e.tilePosition.y);
            }
        }
    }
}
