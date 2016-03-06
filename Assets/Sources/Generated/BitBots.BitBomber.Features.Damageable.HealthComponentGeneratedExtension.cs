using Entitas;

namespace Entitas {
    public partial class Entity {
        public BitBots.BitBomber.Features.Damageable.HealthComponent health { get { return (BitBots.BitBomber.Features.Damageable.HealthComponent)GetComponent(CoreComponentIds.Health); } }

        public bool hasHealth { get { return HasComponent(CoreComponentIds.Health); } }

        public Entity AddHealth(int newHealth) {
            var componentPool = GetComponentPool(CoreComponentIds.Health);
            var component = (BitBots.BitBomber.Features.Damageable.HealthComponent)(componentPool.Count > 0 ? componentPool.Pop() : new BitBots.BitBomber.Features.Damageable.HealthComponent());
            component.health = newHealth;
            return AddComponent(CoreComponentIds.Health, component);
        }

        public Entity ReplaceHealth(int newHealth) {
            var componentPool = GetComponentPool(CoreComponentIds.Health);
            var component = (BitBots.BitBomber.Features.Damageable.HealthComponent)(componentPool.Count > 0 ? componentPool.Pop() : new BitBots.BitBomber.Features.Damageable.HealthComponent());
            component.health = newHealth;
            ReplaceComponent(CoreComponentIds.Health, component);
            return this;
        }

        public Entity RemoveHealth() {
            return RemoveComponent(CoreComponentIds.Health);;
        }
    }
}

    public partial class CoreMatcher {
        static IMatcher _matcherHealth;

        public static IMatcher Health {
            get {
                if (_matcherHealth == null) {
                    var matcher = (Matcher)Matcher.AllOf(CoreComponentIds.Health);
                    matcher.componentNames = CoreComponentIds.componentNames;
                    _matcherHealth = matcher;
                }

                return _matcherHealth;
            }
        }
    }
