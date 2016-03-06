using Entitas;

namespace Entitas {
    public partial class Entity {
        static readonly BitBots.BitBomber.Features.Movement.MoveComponent moveComponent = new BitBots.BitBomber.Features.Movement.MoveComponent();

        public bool isMove {
            get { return HasComponent(CoreComponentIds.Move); }
            set {
                if (value != isMove) {
                    if (value) {
                        AddComponent(CoreComponentIds.Move, moveComponent);
                    } else {
                        RemoveComponent(CoreComponentIds.Move);
                    }
                }
            }
        }

        public Entity IsMove(bool value) {
            isMove = value;
            return this;
        }
    }
}

    public partial class CoreMatcher {
        static IMatcher _matcherMove;

        public static IMatcher Move {
            get {
                if (_matcherMove == null) {
                    var matcher = (Matcher)Matcher.AllOf(CoreComponentIds.Move);
                    matcher.componentNames = CoreComponentIds.componentNames;
                    _matcherMove = matcher;
                }

                return _matcherMove;
            }
        }
    }
