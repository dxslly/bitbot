using Entitas;
using System.Collections.Generic;
using UnityEngine;
using Jint;

namespace BitBots.BitBomber.Features.PlayerAI
{
    public class ExecuteAIOnGameTickSystem : ISetPool, IInitializeSystem
    {
        private JintEngine jintEngine = new JintEngine();
        private Group _aiPlayers;
        
        public void SetPool(Pool pool)
        {
            var gameTick = pool.GetGroup(CoreMatcher.GameTick);
            gameTick.OnEntityAdded += ((group, entity, index, component) => OnGameTick());
            
            _aiPlayers = pool.GetGroup(Matcher.AllOf(CoreMatcher.PlayerAI, CoreMatcher.Player));
        }
        
        public void Initialize()
        {}
        
        private void OnGameTick()
        {
            foreach (var e in _aiPlayers.GetEntities())
            {
                // Create Event
                
                // Execute each AI
                e.playerAI.engine.CallFunction("AI.OnGameTick");
                
                // Execute AI Actions on Game   
            }
        }
    }
}