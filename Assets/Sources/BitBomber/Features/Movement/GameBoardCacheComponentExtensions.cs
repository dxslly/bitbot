using Entitas;
using BitBots.BitBomber.Features.GameBoard;

namespace BitBots.BitBomber.Features.Movement
{
    public static class GameBoardCacheComponentExtensions
    {
        public static bool IsValidMoveCell(this GameBoardCacheComponent cache, GameBoardComponent board, int x, int y)
        {
            if (x < 0 || x >= board.width || y < 0 || y >= board.height)
            {
                return false;
            }
            
            // Ensure that grid does not contain a collidable element
            Entity[,] grid = cache.grid;
            Entity boardElement = grid[x, y];
            
            // Ensure board elment exist and is non-collidable
            if (null != boardElement && boardElement.isCollideable)
            {
                return false;
            }
            
            return true;
        }
    }
}