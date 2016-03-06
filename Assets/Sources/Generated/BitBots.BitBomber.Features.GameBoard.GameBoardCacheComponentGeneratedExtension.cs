using Entitas;

namespace Entitas {
    public partial class Entity {
        public BitBots.BitBomber.Features.GameBoard.GameBoardCacheComponent gameBoardCache { get { return (BitBots.BitBomber.Features.GameBoard.GameBoardCacheComponent)GetComponent(CoreComponentIds.GameBoardCache); } }

        public bool hasGameBoardCache { get { return HasComponent(CoreComponentIds.GameBoardCache); } }

        public Entity AddGameBoardCache(Entitas.Entity[,] newGrid) {
            var componentPool = GetComponentPool(CoreComponentIds.GameBoardCache);
            var component = (BitBots.BitBomber.Features.GameBoard.GameBoardCacheComponent)(componentPool.Count > 0 ? componentPool.Pop() : new BitBots.BitBomber.Features.GameBoard.GameBoardCacheComponent());
            component.grid = newGrid;
            return AddComponent(CoreComponentIds.GameBoardCache, component);
        }

        public Entity ReplaceGameBoardCache(Entitas.Entity[,] newGrid) {
            var componentPool = GetComponentPool(CoreComponentIds.GameBoardCache);
            var component = (BitBots.BitBomber.Features.GameBoard.GameBoardCacheComponent)(componentPool.Count > 0 ? componentPool.Pop() : new BitBots.BitBomber.Features.GameBoard.GameBoardCacheComponent());
            component.grid = newGrid;
            ReplaceComponent(CoreComponentIds.GameBoardCache, component);
            return this;
        }

        public Entity RemoveGameBoardCache() {
            return RemoveComponent(CoreComponentIds.GameBoardCache);;
        }
    }

    public partial class Pool {
        public Entity gameBoardCacheEntity { get { return GetGroup(CoreMatcher.GameBoardCache).GetSingleEntity(); } }

        public BitBots.BitBomber.Features.GameBoard.GameBoardCacheComponent gameBoardCache { get { return gameBoardCacheEntity.gameBoardCache; } }

        public bool hasGameBoardCache { get { return gameBoardCacheEntity != null; } }

        public Entity SetGameBoardCache(Entitas.Entity[,] newGrid) {
            if (hasGameBoardCache) {
                throw new EntitasException("Could not set gameBoardCache!\n" + this + " already has an entity with BitBots.BitBomber.Features.GameBoard.GameBoardCacheComponent!",
                    "You should check if the pool already has a gameBoardCacheEntity before setting it or use pool.ReplaceGameBoardCache().");
            }
            var entity = CreateEntity();
            entity.AddGameBoardCache(newGrid);
            return entity;
        }

        public Entity ReplaceGameBoardCache(Entitas.Entity[,] newGrid) {
            var entity = gameBoardCacheEntity;
            if (entity == null) {
                entity = SetGameBoardCache(newGrid);
            } else {
                entity.ReplaceGameBoardCache(newGrid);
            }

            return entity;
        }

        public void RemoveGameBoardCache() {
            DestroyEntity(gameBoardCacheEntity);
        }
    }
}

    public partial class CoreMatcher {
        static IMatcher _matcherGameBoardCache;

        public static IMatcher GameBoardCache {
            get {
                if (_matcherGameBoardCache == null) {
                    var matcher = (Matcher)Matcher.AllOf(CoreComponentIds.GameBoardCache);
                    matcher.componentNames = CoreComponentIds.componentNames;
                    _matcherGameBoardCache = matcher;
                }

                return _matcherGameBoardCache;
            }
        }
    }
