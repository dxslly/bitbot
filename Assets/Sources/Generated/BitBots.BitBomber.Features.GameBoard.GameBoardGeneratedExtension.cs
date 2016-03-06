using Entitas;

namespace Entitas {
    public partial class Entity {
        public BitBots.BitBomber.Features.GameBoard.GameBoard gameBoard { get { return (BitBots.BitBomber.Features.GameBoard.GameBoard)GetComponent(CoreComponentIds.GameBoard); } }

        public bool hasGameBoard { get { return HasComponent(CoreComponentIds.GameBoard); } }

        public Entity AddGameBoard(int newWidth, int newHeight) {
            var componentPool = GetComponentPool(CoreComponentIds.GameBoard);
            var component = (BitBots.BitBomber.Features.GameBoard.GameBoard)(componentPool.Count > 0 ? componentPool.Pop() : new BitBots.BitBomber.Features.GameBoard.GameBoard());
            component.width = newWidth;
            component.height = newHeight;
            return AddComponent(CoreComponentIds.GameBoard, component);
        }

        public Entity ReplaceGameBoard(int newWidth, int newHeight) {
            var componentPool = GetComponentPool(CoreComponentIds.GameBoard);
            var component = (BitBots.BitBomber.Features.GameBoard.GameBoard)(componentPool.Count > 0 ? componentPool.Pop() : new BitBots.BitBomber.Features.GameBoard.GameBoard());
            component.width = newWidth;
            component.height = newHeight;
            ReplaceComponent(CoreComponentIds.GameBoard, component);
            return this;
        }

        public Entity RemoveGameBoard() {
            return RemoveComponent(CoreComponentIds.GameBoard);;
        }
    }

    public partial class Pool {
        public Entity gameBoardEntity { get { return GetGroup(CoreMatcher.GameBoard).GetSingleEntity(); } }

        public BitBots.BitBomber.Features.GameBoard.GameBoard gameBoard { get { return gameBoardEntity.gameBoard; } }

        public bool hasGameBoard { get { return gameBoardEntity != null; } }

        public Entity SetGameBoard(int newWidth, int newHeight) {
            if (hasGameBoard) {
                throw new EntitasException("Could not set gameBoard!\n" + this + " already has an entity with BitBots.BitBomber.Features.GameBoard.GameBoard!",
                    "You should check if the pool already has a gameBoardEntity before setting it or use pool.ReplaceGameBoard().");
            }
            var entity = CreateEntity();
            entity.AddGameBoard(newWidth, newHeight);
            return entity;
        }

        public Entity ReplaceGameBoard(int newWidth, int newHeight) {
            var entity = gameBoardEntity;
            if (entity == null) {
                entity = SetGameBoard(newWidth, newHeight);
            } else {
                entity.ReplaceGameBoard(newWidth, newHeight);
            }

            return entity;
        }

        public void RemoveGameBoard() {
            DestroyEntity(gameBoardEntity);
        }
    }
}

    public partial class CoreMatcher {
        static IMatcher _matcherGameBoard;

        public static IMatcher GameBoard {
            get {
                if (_matcherGameBoard == null) {
                    var matcher = (Matcher)Matcher.AllOf(CoreComponentIds.GameBoard);
                    matcher.componentNames = CoreComponentIds.componentNames;
                    _matcherGameBoard = matcher;
                }

                return _matcherGameBoard;
            }
        }
    }
