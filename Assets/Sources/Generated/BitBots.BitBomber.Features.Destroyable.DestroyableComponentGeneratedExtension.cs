using Entitas;

namespace Entitas {
    public partial class Entity {
        static readonly BitBots.BitBomber.Features.Destroyable.DestroyableComponent destroyableComponent = new BitBots.BitBomber.Features.Destroyable.DestroyableComponent();

        public bool isDestroyable {
            get { return HasComponent(CoreComponentIds.Destroyable); }
            set {
                if (value != isDestroyable) {
                    if (value) {
                        AddComponent(CoreComponentIds.Destroyable, destroyableComponent);
                    } else {
                        RemoveComponent(CoreComponentIds.Destroyable);
                    }
                }
            }
        }

        public Entity IsDestroyable(bool value) {
            isDestroyable = value;
            return this;
        }
    }
}

    public partial class CoreMatcher {
        static IMatcher _matcherDestroyable;

        public static IMatcher Destroyable {
            get {
                if (_matcherDestroyable == null) {
                    var matcher = (Matcher)Matcher.AllOf(CoreComponentIds.Destroyable);
                    matcher.componentNames = CoreComponentIds.componentNames;
                    _matcherDestroyable = matcher;
                }

                return _matcherDestroyable;
            }
        }
    }
