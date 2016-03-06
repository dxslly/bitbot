using Entitas;
using System.Collections.Generic;
using Jint;
using UnityEngine;
using BitBots.BitBomber.Features.Movement;

namespace BitBots.BitBomber.Features.Bomb
{
    public class TickBombFuseSystem : ISetPool, IInitializeSystem
    {
        private Group _bombs;
        
        public void SetPool(Pool pool)
        {
            var gameTick = pool.GetGroup(CoreMatcher.GameTick);
            gameTick.OnEntityAdded += ((group, entity, index, component) => OnGameTick());
            
            _bombs = pool.GetGroup(Matcher.AllOf(CoreMatcher.Bomb, CoreMatcher.TilePosition));
        }
        
        public void Initialize()
        {}
        
        private void OnGameTick()
        {
            foreach (var e in _bombs.GetEntities())
            {
                var newFuseTime = e.bomb.remainingFuseTime - 1;
                
                if (0 == newFuseTime)
                {
                    // Create Explosion
                    e.destroy();
                }
                else
                {
                    e.ReplaceBomb(newFuseTime);
                }
            }
        }
    }
}