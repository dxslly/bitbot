using Entitas;

namespace BitBots.BitBomber.Features.GameBoard
{
    public class CreateGameBoardCacheSystem : IInitializeSystem, IReactiveSystem, ISetPool
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
            get { return Matcher.AllOf(CoreMatcher.TilePosition, CoreMatcher.GameBoardElement).OnEntityAdded(); }
        }
        
        public void Initialize()
        {
            UpdateGridCache();
        }

        public void Execute(System.Collections.Generic.List<Entity> entities)
        {
            UpdateGridCache();
        }
        
        private void UpdateGridCache()
        {
            var grid = new Entity[_pool.gameBoard.width, _pool.gameBoard.height];
            foreach (var e in _gameBoardElements.GetEntities())
            {
                var pos = e.tilePosition;
                grid[pos.x, pos.y] = e;
            }
            
            _pool.ReplaceGameBoardCache(grid);
        }
    }
}