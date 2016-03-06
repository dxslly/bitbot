using Entitas;

namespace Entitas {
    public partial class Entity {
        public BitBots.BitBomber.Features.GameTick.GameTickComponent gameTick { get { return (BitBots.BitBomber.Features.GameTick.GameTickComponent)GetComponent(CoreComponentIds.GameTick); } }

        public bool hasGameTick { get { return HasComponent(CoreComponentIds.GameTick); } }

        public Entity AddGameTick(int newTurn) {
            var componentPool = GetComponentPool(CoreComponentIds.GameTick);
            var component = (BitBots.BitBomber.Features.GameTick.GameTickComponent)(componentPool.Count > 0 ? componentPool.Pop() : new BitBots.BitBomber.Features.GameTick.GameTickComponent());
            component.turn = newTurn;
            return AddComponent(CoreComponentIds.GameTick, component);
        }

        public Entity ReplaceGameTick(int newTurn) {
            var componentPool = GetComponentPool(CoreComponentIds.GameTick);
            var component = (BitBots.BitBomber.Features.GameTick.GameTickComponent)(componentPool.Count > 0 ? componentPool.Pop() : new BitBots.BitBomber.Features.GameTick.GameTickComponent());
            component.turn = newTurn;
            ReplaceComponent(CoreComponentIds.GameTick, component);
            return this;
        }

        public Entity RemoveGameTick() {
            return RemoveComponent(CoreComponentIds.GameTick);;
        }
    }

    public partial class Pool {
        public Entity gameTickEntity { get { return GetGroup(CoreMatcher.GameTick).GetSingleEntity(); } }

        public BitBots.BitBomber.Features.GameTick.GameTickComponent gameTick { get { return gameTickEntity.gameTick; } }

        public bool hasGameTick { get { return gameTickEntity != null; } }

        public Entity SetGameTick(int newTurn) {
            if (hasGameTick) {
                throw new EntitasException("Could not set gameTick!\n" + this + " already has an entity with BitBots.BitBomber.Features.GameTick.GameTickComponent!",
                    "You should check if the pool already has a gameTickEntity before setting it or use pool.ReplaceGameTick().");
            }
            var entity = CreateEntity();
            entity.AddGameTick(newTurn);
            return entity;
        }

        public Entity ReplaceGameTick(int newTurn) {
            var entity = gameTickEntity;
            if (entity == null) {
                entity = SetGameTick(newTurn);
            } else {
                entity.ReplaceGameTick(newTurn);
            }

            return entity;
        }

        public void RemoveGameTick() {
            DestroyEntity(gameTickEntity);
        }
    }
}

    public partial class CoreMatcher {
        static IMatcher _matcherGameTick;

        public static IMatcher GameTick {
            get {
                if (_matcherGameTick == null) {
                    var matcher = (Matcher)Matcher.AllOf(CoreComponentIds.GameTick);
                    matcher.componentNames = CoreComponentIds.componentNames;
                    _matcherGameTick = matcher;
                }

                return _matcherGameTick;
            }
        }
    }
