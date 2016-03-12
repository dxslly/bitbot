using Entitas;
using System.Collections.Generic;
using UnityEngine;
using BitBots.BitBomber.Features.Movement;

namespace BitBots.BitBomber.Features.Bomb
{
    public class ExplodeBombSysem : ISetPool, IReactiveSystem
    {
        private Pool _pool;
        
        public void SetPool(Pool pool)
        {
            _pool = pool;
        }
        
        public TriggerOnEvent trigger
        {
            get { return Matcher.AllOf(CoreMatcher.Bomb, CoreMatcher.Destroyable, CoreMatcher.TilePosition).OnEntityAdded(); }
        }
        
        public void Execute(List<Entity> entities)
        {
            foreach (var e in entities)
            {
                CreateCardinalExplosion(e);
            }
        }
        
        private void CreateCardinalExplosion(Entity e)
        {
            var spread = e.bomb.spread;
            var pos = e.tilePosition;
            
            // Cardinal directions
            _pool.CreateExplosion(pos.x, pos.y);
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
                
                // Ensure explosion may move into the next tile
                if (!grid.IsValidMoveCell(board, x, y))
                {
                    break;
                }
                
                _pool.CreateExplosion(x, y);
            }
        }
    }
}