using Entitas;
using System.Collections.Generic;
using UnityEngine;

namespace BitBots.BitBomber.Features.Player
{
    public class ExecuteAIOnGameTickSystem : IReactiveSystem
    {
        public TriggerOnEvent trigger
        {
            get { return Matcher.AllOf(CoreMatcher.Player).OnEntityAdded(); }
        }
        
        public void Execute(List<Entity> entities)
        {
            foreach (var e in entities)
            {
                // Attempt to move player
            }
        }
    }
}