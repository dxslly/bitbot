using Entitas;

namespace BitBots.BitBomber.Features.Damageable
{
    [CoreAttribute]
    [BitBots.BitBomber.Features.Synchronized.IsSyncData]
    public class HealthComponent : IComponent
    {
        public int health;
    }
}