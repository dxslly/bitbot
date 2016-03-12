using Entitas;

namespace Entitas {
    public partial class Entity {
        public BitBots.BitBomber.Features.Synchronized.SynchronizedComponent synchronized { get { return (BitBots.BitBomber.Features.Synchronized.SynchronizedComponent)GetComponent(CoreComponentIds.Synchronized); } }

        public bool hasSynchronized { get { return HasComponent(CoreComponentIds.Synchronized); } }

        public Entity AddSynchronized(int newId, BitBots.BitBomber.Features.Synchronized.EntityType newType) {
            var componentPool = GetComponentPool(CoreComponentIds.Synchronized);
            var component = (BitBots.BitBomber.Features.Synchronized.SynchronizedComponent)(componentPool.Count > 0 ? componentPool.Pop() : new BitBots.BitBomber.Features.Synchronized.SynchronizedComponent());
            component.id = newId;
            component.Type = newType;
            return AddComponent(CoreComponentIds.Synchronized, component);
        }

        public Entity ReplaceSynchronized(int newId, BitBots.BitBomber.Features.Synchronized.EntityType newType) {
            var componentPool = GetComponentPool(CoreComponentIds.Synchronized);
            var component = (BitBots.BitBomber.Features.Synchronized.SynchronizedComponent)(componentPool.Count > 0 ? componentPool.Pop() : new BitBots.BitBomber.Features.Synchronized.SynchronizedComponent());
            component.id = newId;
            component.Type = newType;
            ReplaceComponent(CoreComponentIds.Synchronized, component);
            return this;
        }

        public Entity RemoveSynchronized() {
            return RemoveComponent(CoreComponentIds.Synchronized);;
        }
    }
}

    public partial class CoreMatcher {
        static IMatcher _matcherSynchronized;

        public static IMatcher Synchronized {
            get {
                if (_matcherSynchronized == null) {
                    var matcher = (Matcher)Matcher.AllOf(CoreComponentIds.Synchronized);
                    matcher.componentNames = CoreComponentIds.componentNames;
                    _matcherSynchronized = matcher;
                }

                return _matcherSynchronized;
            }
        }
    }
