using Entitas;

namespace Entitas {
    public partial class Entity {
        public BitBots.BitBomber.Features.GameTick.GameTickComponent gameTick { get { return (BitBots.BitBomber.Features.GameTick.GameTickComponent)GetComponent(CoreComponentIds.GameTick); } }

        public bool hasGameTick { get { return HasComponent(CoreComponentIds.GameTick); } }

        public Entity AddGameTick(int newTurn) {
            var componentPool = GetComponentPool(CoreComponentIds.GameTick);
            var component = (BitBots.BitBomber.Features.GameTick.GameTickComponent)(componentPool.Count > 0 ? componentPool.Pop() : new BitBots.BitBomber.Features.GameTick.GameTickComponent());
            component.turn = newTurn;
            return AddComponent(CoreComponentIds.GameTick, component);
        }

        public Entity ReplaceGameTick(int newTurn) {
            var componentPool = GetComponentPool(CoreComponentIds.GameTick);
            var component = (BitBots.BitBomber.Features.GameTick.GameTickComponent)(componentPool.Count > 0 ? componentPool.Pop() : new BitBots.BitBomber.Features.GameTick.GameTickComponent());
            component.turn = newTurn;
            ReplaceComponent(CoreComponentIds.GameTick, component);
            return this;
        }

        public Entity RemoveGameTick() {
            return RemoveComponent(CoreComponentIds.GameTick);;
        }
    }
}

    public partial class CoreMatcher {
        static IMatcher _matcherGameTick;

        public static IMatcher GameTick {
            get {
                if (_matcherGameTick == null) {
                    var matcher = (Matcher)Matcher.AllOf(CoreComponentIds.GameTick);
                    matcher.componentNames = CoreComponentIds.componentNames;
                    _matcherGameTick = matcher;
                }

                return _matcherGameTick;
            }
        }
    }
