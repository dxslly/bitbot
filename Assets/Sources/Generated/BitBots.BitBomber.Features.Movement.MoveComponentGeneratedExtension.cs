using Entitas;

namespace Entitas {
    public partial class Entity {
        public BitBots.BitBomber.Features.Movement.MoveComponent move { get { return (BitBots.BitBomber.Features.Movement.MoveComponent)GetComponent(CoreComponentIds.Move); } }

        public bool hasMove { get { return HasComponent(CoreComponentIds.Move); } }

        public Entity AddMove(BitBots.BitBomber.Features.Movement.MoveDirection newMoveDirection) {
            var componentPool = GetComponentPool(CoreComponentIds.Move);
            var component = (BitBots.BitBomber.Features.Movement.MoveComponent)(componentPool.Count > 0 ? componentPool.Pop() : new BitBots.BitBomber.Features.Movement.MoveComponent());
            component.moveDirection = newMoveDirection;
            return AddComponent(CoreComponentIds.Move, component);
        }

        public Entity ReplaceMove(BitBots.BitBomber.Features.Movement.MoveDirection newMoveDirection) {
            var componentPool = GetComponentPool(CoreComponentIds.Move);
            var component = (BitBots.BitBomber.Features.Movement.MoveComponent)(componentPool.Count > 0 ? componentPool.Pop() : new BitBots.BitBomber.Features.Movement.MoveComponent());
            component.moveDirection = newMoveDirection;
            ReplaceComponent(CoreComponentIds.Move, component);
            return this;
        }

        public Entity RemoveMove() {
            return RemoveComponent(CoreComponentIds.Move);;
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
