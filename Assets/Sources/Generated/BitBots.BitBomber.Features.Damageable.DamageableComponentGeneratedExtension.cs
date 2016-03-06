using Entitas;

namespace Entitas {
    public partial class Entity {
        static readonly BitBots.BitBomber.Features.Damageable.DamageableComponent damageableComponent = new BitBots.BitBomber.Features.Damageable.DamageableComponent();

        public bool isDamageable {
            get { return HasComponent(CoreComponentIds.Damageable); }
            set {
                if (value != isDamageable) {
                    if (value) {
                        AddComponent(CoreComponentIds.Damageable, damageableComponent);
                    } else {
                        RemoveComponent(CoreComponentIds.Damageable);
                    }
                }
            }
        }

        public Entity IsDamageable(bool value) {
            isDamageable = value;
            return this;
        }
    }
}

    public partial class CoreMatcher {
        static IMatcher _matcherDamageable;

        public static IMatcher Damageable {
            get {
                if (_matcherDamageable == null) {
                    var matcher = (Matcher)Matcher.AllOf(CoreComponentIds.Damageable);
                    matcher.componentNames = CoreComponentIds.componentNames;
                    _matcherDamageable = matcher;
                }

                return _matcherDamageable;
            }
        }
    }
