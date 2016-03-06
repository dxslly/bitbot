using Entitas;
using UnityEngine;

namespace BitBots.BitBomber.Features.GameTick
{
    public class ExecuteGameTickSystem : IInitializeSystem, IExecuteSystem, ISetPool
    {
        private float _timeElapsedSinceLastTick = 0f;
        private const float TICK_RATE = 0.8f;
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
            _timeElapsedSinceLastTick += Time.deltaTime;
            
            if (_timeElapsedSinceLastTick >= TICK_RATE)
            {
                _timeElapsedSinceLastTick -= TICK_RATE;
                
                int nextTick = _pool.gameTick.turn + 1;
                _pool.ReplaceGameTick(nextTick);
            }
        }
    }
}