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
        private Pool _pool;
        
        public void SetPool(Pool pool)
        {
            _pool = pool;
            
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
                    CreateCardinalExplosion(e);
                    e.destroy();
                }
                else
                {
                    e.ReplaceBomb(newFuseTime, e.bomb.spread);
                }
            }
        }
        
        private void CreateCardinalExplosion(Entity e)
        {
            var spread = e.bomb.spread;
            var pos = e.tilePosition;
            
            // Cardinal directions
            
            ExploadInDirection(e, spread, pos.x, pos.y, -1, 0);
            ExploadInDirection(e, spread, pos.x, pos.y, 1, 0);
            ExploadInDirection(e, spread, pos.x, pos.y, 0, 1);
            ExploadInDirection(e, spread, pos.x, pos.y, 0, -1);
        }
        
        private void ExploadInDirection(Entity e, int depth, int startX, int startY, int deltaX, int deltaY)
        {
            var grid = _pool.gameBoardCache;
            var board = _pool.gameBoard;
            
            for (int i = 0; i < depth; i++)
            {
                int x = startX + deltaX;
                int y = startY + deltaY;
                
                if (!grid.IsValidMoveCell(board, x, y))
                {
                    break;
                }
                
                // Create explosion
            }
        }
    }
}