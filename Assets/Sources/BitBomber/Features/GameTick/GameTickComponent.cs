using Entitas;
using UnityEngine;

namespace BitBots.BitBomber.Features.GameTick
{
    [Core]
    public class GameTickComponent : IComponent
    {
        public int turn;
    }
}