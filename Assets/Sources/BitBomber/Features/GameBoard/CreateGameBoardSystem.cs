using Entitas;

namespace BitBots.BitBomber.Features.GameBoard
{
    public class CreateGameBoardSystem : IInitializeSystem, ISetPool, IReactiveSystem
    {
        private Pool _pool;
        private Group _gameBoardElements;
        
        public void SetPool(Pool pool)
        {
            _pool = pool;
            _gameBoardElements = pool.GetGroup(CoreMatcher.GameBoardElement);
        }
        
        public TriggerOnEvent trigger
        {
            get { return Matcher.AllOf(CoreMatcher.GameBoard).OnEntityAdded(); }
        }
        
        public void Initialize()
        {
            _pool.SetGameBoard(10, 10);
        }
        
        public void Execute(System.Collections.Generic.List<Entity> entities)
        {
            foreach (var e in _gameBoardElements.GetEntities())
            {
                // TODO(David): Add destroy component
                e.destroy();
            }
            
            // Recreate Grid
            var board = _pool.gameBoard;
            int topRow = board.height - 1;
            int rightColumn = board.width - 1;
            int topWalkRow = topRow - 1;
            int rightWalkRow = rightColumn - 1;
            
            for(int x = 0; x < board.width; x++)
            {
                for (int y = 0; y < board.height; y++)
                {
                    // TODO(David): Enforce walkable tiles in corners
                    
                    // Ensure it is not an outside wall
                    if (x == 0 || x == rightColumn || y == 0 || y == topRow)
                    {
                        // Create Solid tile
                        _pool.CreateSolidTitle(x, y);
                    }
                    // Ensure it is not a walk row x == rightWalkRow
                    else if (x == rightWalkRow || y == topWalkRow)
                    {
                        // Create walkable tile
                        _pool.CreateWalkableTile(x, y);
                    }
                    // Ensure not a post
                    else if (0 == (x % 3) && 0 == (y % 3))
                    {
                        _pool.CreateSolidTitle(x, y);
                    }
                    // Otherwise do whatever
                    else
                    {
                        // Create walkable or destroyable tile
                        _pool.CreateWalkableTile(x, y);
                    }
                }
            }
        }
    }
}