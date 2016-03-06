using Entitas;

namespace BitBots.BitBomber.Features.Damageable
{
    public class DamageSystem : ISetPool, IInitializeSystem
    {
        private Pool _pool;
        private Group _damagers;
        private Group _damageable;
        
        public void SetPool(Pool pool)
        {
            _pool = pool;
            
            var gameTick = pool.GetGroup(CoreMatcher.GameTick);
            gameTick.OnEntityAdded += ((group, entity, index, component) => OnGameTick());
            
            _damageable = pool.GetGroup(Matcher.AllOf(CoreMatcher.Damageable, CoreMatcher.Health, CoreMatcher.TilePosition));
            _damagers = pool.GetGroup(Matcher.AllOf(CoreMatcher.Damager, CoreMatcher.TilePosition));
        }
        
        public void Initialize()
        {}
        
        private void OnGameTick()
        {
            // TODO(David): Improve this
            foreach (var damageable in _damageable.GetEntities())
            {
                foreach (var damager in _damagers.GetEntities())
                {
                    if (damageable.tilePosition.x == damager.tilePosition.x &&
                        damageable.tilePosition.y == damager.tilePosition.y)
                    {
                        damageable.ReplaceHealth(damageable.health.health - damager.damager.amount);
                        
                        if (damageable.health.health <= 0)
                        {
                            damageable.IsDestroyable(true);
                        }
                    }
                }
            }
        }
    }
}