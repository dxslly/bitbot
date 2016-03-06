using Entitas;
using System;
using UnityEngine;

namespace BitBots.BitBomber.Features.GameBoard
{
    class CreateGameBoardCacheSystem : IInitializeSystem, IReactiveSystem, ISetPool
    {
        public void SetPool(Pool pool)
        {}
        
        public TriggerOnEvent trigger
        {
            get { return Matcher.AllOf(CoreMatcher.TilePosition).OnEntityAdded(); }
        }
        
        public void Initialize()
        {}

        public void Execute(System.Collections.Generic.List<Entity> entities)
        {
            foreach (var e in entities)
            {}
        }
        
        private void UpdateGrid()
        {}
    }
}