using Entitas;

namespace Entitas {
    public partial class Entity {
        public BitBots.BitBomber.Features.View.PrefabComponent prefab { get { return (BitBots.BitBomber.Features.View.PrefabComponent)GetComponent(CoreComponentIds.Prefab); } }

        public bool hasPrefab { get { return HasComponent(CoreComponentIds.Prefab); } }

        public Entity AddPrefab(string newPath) {
            var componentPool = GetComponentPool(CoreComponentIds.Prefab);
            var component = (BitBots.BitBomber.Features.View.PrefabComponent)(componentPool.Count > 0 ? componentPool.Pop() : new BitBots.BitBomber.Features.View.PrefabComponent());
            component.path = newPath;
            return AddComponent(CoreComponentIds.Prefab, component);
        }

        public Entity ReplacePrefab(string newPath) {
            var componentPool = GetComponentPool(CoreComponentIds.Prefab);
            var component = (BitBots.BitBomber.Features.View.PrefabComponent)(componentPool.Count > 0 ? componentPool.Pop() : new BitBots.BitBomber.Features.View.PrefabComponent());
            component.path = newPath;
            ReplaceComponent(CoreComponentIds.Prefab, component);
            return this;
        }

        public Entity RemovePrefab() {
            return RemoveComponent(CoreComponentIds.Prefab);;
        }
    }
}

    public partial class CoreMatcher {
        static IMatcher _matcherPrefab;

        public static IMatcher Prefab {
            get {
                if (_matcherPrefab == null) {
                    var matcher = (Matcher)Matcher.AllOf(CoreComponentIds.Prefab);
                    matcher.componentNames = CoreComponentIds.componentNames;
                    _matcherPrefab = matcher;
                }

                return _matcherPrefab;
            }
        }
    }
