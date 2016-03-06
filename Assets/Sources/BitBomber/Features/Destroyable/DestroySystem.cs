using Entitas;
using System.Collections.Generic;

namespace BitBots.BitBomber.Features.Destroyable
{
    public class DestroySystem : IReactiveSystem
    {   
        public TriggerOnEvent trigger
        {
            get { return CoreMatcher.Destroyable.OnEntityAdded(); }
        }
        
        public void Execute(List<Entity> entities)
        {
            foreach (var e in entities)
            {
                e.destroy();
            }
        }
    }
}