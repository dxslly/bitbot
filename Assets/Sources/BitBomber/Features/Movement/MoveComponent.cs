using Entitas;

namespace BitBots.BitBomber.Features.Movement
{
    [Core]
    public class MoveComponent : IComponent
    {
        public MoveDirection moveDirection;
    }
}