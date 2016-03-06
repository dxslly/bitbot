using Entitas;
using System;
using UnityEngine;

namespace BitBots.BitBomber.Features.GameBoard
{
    class CreateGameBoardSystem : IInitializeSystem
    {
        public void Initialize()
        {
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    Pools.core.CreateWalkableTile(x, y);
                }
            }
        }
    }
}