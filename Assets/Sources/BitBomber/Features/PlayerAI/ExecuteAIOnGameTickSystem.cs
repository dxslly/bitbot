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
        {
        }
        
        private void OnGameTick()
        {
            foreach (var e in _aiPlayers.GetEntities())
            {
                // Create Event
                e.playerAI.engine.SetFunction("log", new Jint.Delegates.Action<object>(Debug.Log));
                
                // Execute each AI
                e.playerAI.engine.CallFunction("OnGameTick", "TEST");
                
                // Execute AI Actions on Game   
            }
        }
    }
}