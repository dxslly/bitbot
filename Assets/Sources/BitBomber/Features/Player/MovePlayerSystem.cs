using BitBots.BitBomber.Features.Movement;
using Entitas;
using System.Collections.Generic;
using UnityEngine;

namespace BitBots.BitBomber.Features.Player
{
    public class MovePlayerSystem : IReactiveSystem, ISetPool, IEnsureComponents
    {
        private Pool _pool;
        
        public void SetPool(Pool pool)
        {
            _pool = pool;
        }
        
        public TriggerOnEvent trigger
        {
            get { return Matcher.AllOf(CoreMatcher.Player, CoreMatcher.Move, CoreMatcher.TilePosition).OnEntityAdded(); }
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
                Entity[,] grid = _pool.gameBoardCache.grid;
                
                int targetX = e.tilePosition.x;
                int targetY = e.tilePosition.y;
                
                switch (e.move.moveDirection)
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
                
                // Ensure target is within grid bounds
                // if (targetX < 0 || targetX >= grid || targetY < 0 || targetY >= )
                
                // Ensure that grid does not contain a collidable element
                // grid[targetX, targetY]
                
                e.RemoveMove();
            }
        }
    }
}