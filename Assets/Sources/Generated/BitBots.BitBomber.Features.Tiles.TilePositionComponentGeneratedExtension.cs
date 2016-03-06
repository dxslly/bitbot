using Entitas;

namespace Entitas {
    public partial class Entity {
        public BitBots.BitBomber.Features.Tiles.TilePositionComponent tilePosition { get { return (BitBots.BitBomber.Features.Tiles.TilePositionComponent)GetComponent(CoreComponentIds.TilePosition); } }

        public bool hasTilePosition { get { return HasComponent(CoreComponentIds.TilePosition); } }

        public Entity AddTilePosition(int newX, int newY) {
            var componentPool = GetComponentPool(CoreComponentIds.TilePosition);
            var component = (BitBots.BitBomber.Features.Tiles.TilePositionComponent)(componentPool.Count > 0 ? componentPool.Pop() : new BitBots.BitBomber.Features.Tiles.TilePositionComponent());
            component.x = newX;
            component.y = newY;
            return AddComponent(CoreComponentIds.TilePosition, component);
        }

        public Entity ReplaceTilePosition(int newX, int newY) {
            var componentPool = GetComponentPool(CoreComponentIds.TilePosition);
            var component = (BitBots.BitBomber.Features.Tiles.TilePositionComponent)(componentPool.Count > 0 ? componentPool.Pop() : new BitBots.BitBomber.Features.Tiles.TilePositionComponent());
            component.x = newX;
            component.y = newY;
            ReplaceComponent(CoreComponentIds.TilePosition, component);
            return this;
        }

        public Entity RemoveTilePosition() {
            return RemoveComponent(CoreComponentIds.TilePosition);;
        }
    }
}

    public partial class CoreMatcher {
        static IMatcher _matcherTilePosition;

        public static IMatcher TilePosition {
            get {
                if (_matcherTilePosition == null) {
                    var matcher = (Matcher)Matcher.AllOf(CoreComponentIds.TilePosition);
                    matcher.componentNames = CoreComponentIds.componentNames;
                    _matcherTilePosition = matcher;
                }

                return _matcherTilePosition;
            }
        }
    }
