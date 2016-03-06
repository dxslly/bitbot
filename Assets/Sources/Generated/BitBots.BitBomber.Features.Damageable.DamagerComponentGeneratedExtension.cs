using Entitas;

namespace Entitas {
    public partial class Entity {
        public BitBots.BitBomber.Features.Damageable.DamagerComponent damager { get { return (BitBots.BitBomber.Features.Damageable.DamagerComponent)GetComponent(CoreComponentIds.Damager); } }

        public bool hasDamager { get { return HasComponent(CoreComponentIds.Damager); } }

        public Entity AddDamager(int newAmount) {
            var componentPool = GetComponentPool(CoreComponentIds.Damager);
            var component = (BitBots.BitBomber.Features.Damageable.DamagerComponent)(componentPool.Count > 0 ? componentPool.Pop() : new BitBots.BitBomber.Features.Damageable.DamagerComponent());
            component.amount = newAmount;
            return AddComponent(CoreComponentIds.Damager, component);
        }

        public Entity ReplaceDamager(int newAmount) {
            var componentPool = GetComponentPool(CoreComponentIds.Damager);
            var component = (BitBots.BitBomber.Features.Damageable.DamagerComponent)(componentPool.Count > 0 ? componentPool.Pop() : new BitBots.BitBomber.Features.Damageable.DamagerComponent());
            component.amount = newAmount;
            ReplaceComponent(CoreComponentIds.Damager, component);
            return this;
        }

        public Entity RemoveDamager() {
            return RemoveComponent(CoreComponentIds.Damager);;
        }
    }
}

    public partial class CoreMatcher {
        static IMatcher _matcherDamager;

        public static IMatcher Damager {
            get {
                if (_matcherDamager == null) {
                    var matcher = (Matcher)Matcher.AllOf(CoreComponentIds.Damager);
                    matcher.componentNames = CoreComponentIds.componentNames;
                    _matcherDamager = matcher;
                }

                return _matcherDamager;
            }
        }
    }
