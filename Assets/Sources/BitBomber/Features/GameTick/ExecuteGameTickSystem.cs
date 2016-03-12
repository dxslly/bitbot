using Entitas;
using System;
using UnityEngine;

namespace BitBots.BitBomber.Features.GameTick
{
    public class ExecuteGameTickSystem : IInitializeSystem, IExecuteSystem, ISetPool
    {
        private double _timeElapsedSinceLastTick = 0f;
        private const double TICK_RATE = 0.8f;
        private Pool _pool;
        
        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Initialize()
        {
            _pool.SetGameTick(0);
        }
        
        public void Execute()
        {
            ExecuteTick(Time.deltaTime);
        }

        public void ExecuteTick(double span)
        {
            _timeElapsedSinceLastTick += span;
            if (_timeElapsedSinceLastTick >= TICK_RATE)
            {
                _timeElapsedSinceLastTick -= TICK_RATE;

                int nextTick = _pool.gameTick.turn + 1;
                _pool.ReplaceGameTick(nextTick);
            }
        }
    }
}