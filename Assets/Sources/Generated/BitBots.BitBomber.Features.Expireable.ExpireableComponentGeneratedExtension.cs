using Entitas;

namespace Entitas {
    public partial class Entity {
        public BitBots.BitBomber.Features.Expireable.ExpireableComponent expireable { get { return (BitBots.BitBomber.Features.Expireable.ExpireableComponent)GetComponent(CoreComponentIds.Expireable); } }

        public bool hasExpireable { get { return HasComponent(CoreComponentIds.Expireable); } }

        public Entity AddExpireable(int newRemainingTicksToLive) {
            var componentPool = GetComponentPool(CoreComponentIds.Expireable);
            var component = (BitBots.BitBomber.Features.Expireable.ExpireableComponent)(componentPool.Count > 0 ? componentPool.Pop() : new BitBots.BitBomber.Features.Expireable.ExpireableComponent());
            component.remainingTicksToLive = newRemainingTicksToLive;
            return AddComponent(CoreComponentIds.Expireable, component);
        }

        public Entity ReplaceExpireable(int newRemainingTicksToLive) {
            var componentPool = GetComponentPool(CoreComponentIds.Expireable);
            var component = (BitBots.BitBomber.Features.Expireable.ExpireableComponent)(componentPool.Count > 0 ? componentPool.Pop() : new BitBots.BitBomber.Features.Expireable.ExpireableComponent());
            component.remainingTicksToLive = newRemainingTicksToLive;
            ReplaceComponent(CoreComponentIds.Expireable, component);
            return this;
        }

        public Entity RemoveExpireable() {
            return RemoveComponent(CoreComponentIds.Expireable);;
        }
    }
}

    public partial class CoreMatcher {
        static IMatcher _matcherExpireable;

        public static IMatcher Expireable {
            get {
                if (_matcherExpireable == null) {
                    var matcher = (Matcher)Matcher.AllOf(CoreComponentIds.Expireable);
                    matcher.componentNames = CoreComponentIds.componentNames;
                    _matcherExpireable = matcher;
                }

                return _matcherExpireable;
            }
        }
    }
