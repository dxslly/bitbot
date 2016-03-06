using Entitas;

namespace BitBots.BitBomber.Features.Damageable
{
    [CoreAttribute]
    public class DamagerComponent : IComponent
    {
        public int amount;
    }
}