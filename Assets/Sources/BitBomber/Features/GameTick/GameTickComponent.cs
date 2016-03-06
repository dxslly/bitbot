using Entitas;
using UnityEngine;
using Entitas.CodeGenerator;

namespace BitBots.BitBomber.Features.GameTick
{
    [Core]
    [SingleEntity]
    public class GameTickComponent : IComponent
    {
        public int turn;
    }
}