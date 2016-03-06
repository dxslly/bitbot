using Entitas;

namespace Entitas {
    public partial class Entity {
        public BitBots.BitBomber.Features.Owner.OwnerComponent owner { get { return (BitBots.BitBomber.Features.Owner.OwnerComponent)GetComponent(CoreComponentIds.Owner); } }

        public bool hasOwner { get { return HasComponent(CoreComponentIds.Owner); } }

        public Entity AddOwner(Entitas.Entity newOwner) {
            var componentPool = GetComponentPool(CoreComponentIds.Owner);
            var component = (BitBots.BitBomber.Features.Owner.OwnerComponent)(componentPool.Count > 0 ? componentPool.Pop() : new BitBots.BitBomber.Features.Owner.OwnerComponent());
            component.owner = newOwner;
            return AddComponent(CoreComponentIds.Owner, component);
        }

        public Entity ReplaceOwner(Entitas.Entity newOwner) {
            var componentPool = GetComponentPool(CoreComponentIds.Owner);
            var component = (BitBots.BitBomber.Features.Owner.OwnerComponent)(componentPool.Count > 0 ? componentPool.Pop() : new BitBots.BitBomber.Features.Owner.OwnerComponent());
            component.owner = newOwner;
            ReplaceComponent(CoreComponentIds.Owner, component);
            return this;
        }

        public Entity RemoveOwner() {
            return RemoveComponent(CoreComponentIds.Owner);;
        }
    }
}

    public partial class CoreMatcher {
        static IMatcher _matcherOwner;

        public static IMatcher Owner {
            get {
                if (_matcherOwner == null) {
                    var matcher = (Matcher)Matcher.AllOf(CoreComponentIds.Owner);
                    matcher.componentNames = CoreComponentIds.componentNames;
                    _matcherOwner = matcher;
                }

                return _matcherOwner;
            }
        }
    }
