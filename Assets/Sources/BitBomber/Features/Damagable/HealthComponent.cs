using Entitas;

namespace BitBots.BitBomber.Features.Damageable
{
    [CoreAttribute]
    public class HealthComponent : IComponent
    {
        public int health;
    }
}