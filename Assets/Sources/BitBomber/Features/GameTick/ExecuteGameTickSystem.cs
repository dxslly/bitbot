using Entitas;
using System;
using UnityEngine;

namespace BitBots.BitBomber.Features.GameTick
{
    public class ExecuteGameTickSystem : IExecuteSystem, ISetPool
    {
        private float _timeElapsedSinceLastTick = 0f;
        private const float TICK_RATE = 0.8f;
        private int _currentGameTick = 0;
        private Pool _pool;
        
        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Execute()
        {
            _timeElapsedSinceLastTick += Time.deltaTime;
            
            if (_timeElapsedSinceLastTick >= TICK_RATE)
            {
                _timeElapsedSinceLastTick -= TICK_RATE;
                
                _currentGameTick += 1;
                
                _pool.CreateEntity()
                    .AddGameTick(_currentGameTick);
            }
        }
    }
}