using Entitas;

namespace Entitas {
    public partial class Entity {
        public BitBots.BitBomber.Features.Bomb.BombComponent bomb { get { return (BitBots.BitBomber.Features.Bomb.BombComponent)GetComponent(CoreComponentIds.Bomb); } }

        public bool hasBomb { get { return HasComponent(CoreComponentIds.Bomb); } }

        public Entity AddBomb(int newSpread) {
            var componentPool = GetComponentPool(CoreComponentIds.Bomb);
            var component = (BitBots.BitBomber.Features.Bomb.BombComponent)(componentPool.Count > 0 ? componentPool.Pop() : new BitBots.BitBomber.Features.Bomb.BombComponent());
            component.spread = newSpread;
            return AddComponent(CoreComponentIds.Bomb, component);
        }

        public Entity ReplaceBomb(int newSpread) {
            var componentPool = GetComponentPool(CoreComponentIds.Bomb);
            var component = (BitBots.BitBomber.Features.Bomb.BombComponent)(componentPool.Count > 0 ? componentPool.Pop() : new BitBots.BitBomber.Features.Bomb.BombComponent());
            component.spread = newSpread;
            ReplaceComponent(CoreComponentIds.Bomb, component);
            return this;
        }

        public Entity RemoveBomb() {
            return RemoveComponent(CoreComponentIds.Bomb);;
        }
    }
}

    public partial class CoreMatcher {
        static IMatcher _matcherBomb;

        public static IMatcher Bomb {
            get {
                if (_matcherBomb == null) {
                    var matcher = (Matcher)Matcher.AllOf(CoreComponentIds.Bomb);
                    matcher.componentNames = CoreComponentIds.componentNames;
                    _matcherBomb = matcher;
                }

                return _matcherBomb;
            }
        }
    }
