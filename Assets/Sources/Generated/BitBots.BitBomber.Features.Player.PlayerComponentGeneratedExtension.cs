using Entitas;

namespace Entitas {
    public partial class Entity {
        public BitBots.BitBomber.Features.Player.PlayerComponent player { get { return (BitBots.BitBomber.Features.Player.PlayerComponent)GetComponent(CoreComponentIds.Player); } }

        public bool hasPlayer { get { return HasComponent(CoreComponentIds.Player); } }

        public Entity AddPlayer(string newPlayerID) {
            var componentPool = GetComponentPool(CoreComponentIds.Player);
            var component = (BitBots.BitBomber.Features.Player.PlayerComponent)(componentPool.Count > 0 ? componentPool.Pop() : new BitBots.BitBomber.Features.Player.PlayerComponent());
            component.playerID = newPlayerID;
            return AddComponent(CoreComponentIds.Player, component);
        }

        public Entity ReplacePlayer(string newPlayerID) {
            var componentPool = GetComponentPool(CoreComponentIds.Player);
            var component = (BitBots.BitBomber.Features.Player.PlayerComponent)(componentPool.Count > 0 ? componentPool.Pop() : new BitBots.BitBomber.Features.Player.PlayerComponent());
            component.playerID = newPlayerID;
            ReplaceComponent(CoreComponentIds.Player, component);
            return this;
        }

        public Entity RemovePlayer() {
            return RemoveComponent(CoreComponentIds.Player);;
        }
    }
}

    public partial class CoreMatcher {
        static IMatcher _matcherPlayer;

        public static IMatcher Player {
            get {
                if (_matcherPlayer == null) {
                    var matcher = (Matcher)Matcher.AllOf(CoreComponentIds.Player);
                    matcher.componentNames = CoreComponentIds.componentNames;
                    _matcherPlayer = matcher;
                }

                return _matcherPlayer;
            }
        }
    }
