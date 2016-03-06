using Entitas;
using System.Collections.Generic;
using Jint;
using UnityEngine;

namespace BitBots.BitBomber.Features.Expireable
{
    public class ExpireSystem : ISetPool, IInitializeSystem
    {
        private Group _expireables;
        private Pool _pool;
        
        public void SetPool(Pool pool)
        {
            _pool = pool;
            
            var gameTick = pool.GetGroup(CoreMatcher.GameTick);
            gameTick.OnEntityAdded += ((group, entity, index, component) => OnGameTick());
            
            _expireables = pool.GetGroup(CoreMatcher.Expireable);
        }
        
        public void Initialize()
        {}
        
        private void OnGameTick()
        {
            foreach (var e in _expireables.GetEntities())
            {
                var remainingTicks = e.expireable.remainingTicksToLive - 1;
                
                if (0 <= remainingTicks)
                {
                    e.IsDestroyable(true);
                    continue;
                }
                
                e.ReplaceExpireable(remainingTicks);
            }
        }
    }
}