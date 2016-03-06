using Entitas;
using System.Collections.Generic;
using Jint;
using UnityEngine;
using BitBots.BitBomber.Features.Movement;

namespace BitBots.BitBomber.Features.PlayerAI
{
    public class ExecuteAIOnGameTickSystem : ISetPool, IReactiveSystem
    {
        private Group _aiPlayers;
        
        public void SetPool(Pool pool)
        {
            var gameTick = pool.GetGroup(CoreMatcher.GameTick);
            gameTick.OnEntityAdded += ((group, entity, index, component) => OnGameTick());
            
            _aiPlayers = pool.GetGroup(Matcher.AllOf(CoreMatcher.PlayerAI, CoreMatcher.Player));
        }
        
        public TriggerOnEvent trigger
        {
            get { return CoreMatcher.PlayerAI.OnEntityAdded(); }
        }
        
        public void Execute(List<Entity> entities)
        {
            foreach (var e in entities)
            {
                // Add Hooks to PlayerAI
                e.playerAI.engine.SetFunction("move", new Jint.Delegates.Action<string>((movement) => MovePlayer(e, movement)));
            }
        }
        
        private void OnGameTick()
        {
            foreach (var e in _aiPlayers.GetEntities())
            {
                Debug.Log("OnTickPlayer: " + e.player.playerID);
                
                // Execute each AI
                try
                {
                    e.playerAI.engine.CallFunction("OnGameTick", "TEST");
                }
                catch (System.Exception exception)
                {
                    // TODO(David): Track number of errors
                    Debug.Log("Error Executing Bot: " + e.player.playerID);
                    Debug.Log(exception);
                }
            }
        }
        
        private void MovePlayer(Entity entity, string movement)
        {
            Debug.Log("Moving: " + entity.player.playerID + " " + movement);
            
            MoveDirection moveDirection = movement.ToMoveDirection();
            
            if (entity.hasMove)
            {
                entity.ReplaceMove(moveDirection);
            }
            else
            {
                entity.AddMove(moveDirection);
            }
        }
    }
}