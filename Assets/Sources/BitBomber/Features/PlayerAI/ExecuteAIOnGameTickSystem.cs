using Entitas;
using System.Collections.Generic;
using Jint;
using BitBots.BitBomber.Features.Movement;

namespace BitBots.BitBomber.Features.PlayerAI
{
    public class ExecuteAIOnGameTickSystem : ISetPool, IReactiveSystem
    {
        private JintEngine jintEngine = new JintEngine();
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
                // Execute each AI
                e.playerAI.engine.CallFunction("OnGameTick", "TEST");
            }
        }
        
        private void MovePlayer(Entity entity, string movement)
        {
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