using Entitas;

namespace Entitas {
    public partial class Entity {
        public BitBots.BitBomber.Features.View.ViewComponent view { get { return (BitBots.BitBomber.Features.View.ViewComponent)GetComponent(CoreComponentIds.View); } }

        public bool hasView { get { return HasComponent(CoreComponentIds.View); } }

        public Entity AddView(UnityEngine.GameObject newGameObject) {
            var componentPool = GetComponentPool(CoreComponentIds.View);
            var component = (BitBots.BitBomber.Features.View.ViewComponent)(componentPool.Count > 0 ? componentPool.Pop() : new BitBots.BitBomber.Features.View.ViewComponent());
            component.gameObject = newGameObject;
            return AddComponent(CoreComponentIds.View, component);
        }

        public Entity ReplaceView(UnityEngine.GameObject newGameObject) {
            var componentPool = GetComponentPool(CoreComponentIds.View);
            var component = (BitBots.BitBomber.Features.View.ViewComponent)(componentPool.Count > 0 ? componentPool.Pop() : new BitBots.BitBomber.Features.View.ViewComponent());
            component.gameObject = newGameObject;
            ReplaceComponent(CoreComponentIds.View, component);
            return this;
        }

        public Entity RemoveView() {
            return RemoveComponent(CoreComponentIds.View);;
        }
    }
}

    public partial class CoreMatcher {
        static IMatcher _matcherView;

        public static IMatcher View {
            get {
                if (_matcherView == null) {
                    var matcher = (Matcher)Matcher.AllOf(CoreComponentIds.View);
                    matcher.componentNames = CoreComponentIds.componentNames;
                    _matcherView = matcher;
                }

                return _matcherView;
            }
        }
    }
