using Entitas;

namespace Entitas {
    public partial class Entity {
        static readonly BitBots.BitBomber.Features.Collideable.CollideableComponent collideableComponent = new BitBots.BitBomber.Features.Collideable.CollideableComponent();

        public bool isCollideable {
            get { return HasComponent(CoreComponentIds.Collideable); }
            set {
                if (value != isCollideable) {
                    if (value) {
                        AddComponent(CoreComponentIds.Collideable, collideableComponent);
                    } else {
                        RemoveComponent(CoreComponentIds.Collideable);
                    }
                }
            }
        }

        public Entity IsCollideable(bool value) {
            isCollideable = value;
            return this;
        }
    }
}

    public partial class CoreMatcher {
        static IMatcher _matcherCollideable;

        public static IMatcher Collideable {
            get {
                if (_matcherCollideable == null) {
                    var matcher = (Matcher)Matcher.AllOf(CoreComponentIds.Collideable);
                    matcher.componentNames = CoreComponentIds.componentNames;
                    _matcherCollideable = matcher;
                }

                return _matcherCollideable;
            }
        }
    }
