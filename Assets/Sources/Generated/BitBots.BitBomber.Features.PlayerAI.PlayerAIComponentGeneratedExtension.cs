using Entitas;

namespace Entitas {
    public partial class Entity {
        public BitBots.BitBomber.Features.PlayerAI.PlayerAIComponent playerAI { get { return (BitBots.BitBomber.Features.PlayerAI.PlayerAIComponent)GetComponent(CoreComponentIds.PlayerAI); } }

        public bool hasPlayerAI { get { return HasComponent(CoreComponentIds.PlayerAI); } }

        public Entity AddPlayerAI(BitBots.BitBomber.Features.PlayerAI.BLScript newEngine) {
            var componentPool = GetComponentPool(CoreComponentIds.PlayerAI);
            var component = (BitBots.BitBomber.Features.PlayerAI.PlayerAIComponent)(componentPool.Count > 0 ? componentPool.Pop() : new BitBots.BitBomber.Features.PlayerAI.PlayerAIComponent());
            component.engine = newEngine;
            return AddComponent(CoreComponentIds.PlayerAI, component);
        }

        public Entity ReplacePlayerAI(BitBots.BitBomber.Features.PlayerAI.BLScript newEngine) {
            var componentPool = GetComponentPool(CoreComponentIds.PlayerAI);
            var component = (BitBots.BitBomber.Features.PlayerAI.PlayerAIComponent)(componentPool.Count > 0 ? componentPool.Pop() : new BitBots.BitBomber.Features.PlayerAI.PlayerAIComponent());
            component.engine = newEngine;
            ReplaceComponent(CoreComponentIds.PlayerAI, component);
            return this;
        }

        public Entity RemovePlayerAI() {
            return RemoveComponent(CoreComponentIds.PlayerAI);;
        }
    }
}

    public partial class CoreMatcher {
        static IMatcher _matcherPlayerAI;

        public static IMatcher PlayerAI {
            get {
                if (_matcherPlayerAI == null) {
                    var matcher = (Matcher)Matcher.AllOf(CoreComponentIds.PlayerAI);
                    matcher.componentNames = CoreComponentIds.componentNames;
                    _matcherPlayerAI = matcher;
                }

                return _matcherPlayerAI;
            }
        }
    }
