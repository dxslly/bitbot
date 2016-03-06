using BitBots.BitBomber.Features.Movement;
using Entitas;
using System.Collections.Generic;
using UnityEngine;

namespace BitBots.BitBomber.Features.Movement
{
    public class MoveSystem : IReactiveSystem, ISetPool, IEnsureComponents
    {
        private Pool _pool;
        
        public void SetPool(Pool pool)
        {
            _pool = pool;
        }
        
        public TriggerOnEvent trigger
        {
            get { return Matcher.AllOf(CoreMatcher.Move, CoreMatcher.TilePosition).OnEntityAdded(); }
        }
        
        public IMatcher ensureComponents
        {
            get { return Matcher.AllOf(CoreMatcher.Move, CoreMatcher.TilePosition); }
        }
        
        public void Execute(List<Entity> entities)
        {
            foreach (var e in entities)
            {
                // Attempt to move player
                int targetX = e.tilePosition.x;
                int targetY = e.tilePosition.y;
                
                MoveDirection direction = e.move.moveDirection;
                e.RemoveMove();
                
                // Ensure moveDirection is non-none
                if (MoveDirection.None == direction)
                {
                    continue;
                }
                
                // Determine goal position
                switch (direction)
                {
                case MoveDirection.Down:
                    targetY -= 1;
                    break;
                case MoveDirection.Up:
                    targetY += 1;
                    break;
                case MoveDirection.Right:
                    targetX += 1;
                    break;
                case MoveDirection.Left:
                    targetX -= 1;
                    break;
                }
                
                // Ensure is not moving out of bounds
                var board = _pool.gameBoard;
                if (targetX < 0 || targetX >= board.width || targetY < 0 || targetY >= board.height)
                {
                    continue;
                }
                
                // Ensure that grid does not contain a collidable element
                Entity[,] grid = _pool.gameBoardCache.grid;
                Entity boardElement = grid[targetX, targetY];
                
                // Ensure board elment exist and is non-collidable
                if (null != boardElement && boardElement.isCollideable)
                {
                    continue;
                }
                
                e.ReplaceTilePosition(targetX, targetY);
            }
        }
    }
}