using Entitas;
using System.Collections.Generic;
using UnityEngine;
using BitBots.BitBomber.Features.Movement;
using BitBots.BitBomber.Features.Bomb;
using System.Linq;

namespace BitBots.BitBomber.Features.PlayerAI
{
    public class ExecuteAIOnGameTickSystem : ISetPool, IReactiveSystem
    {
        private Pool _pool;
        private Group _aiPlayers;
        
        public void SetPool(Pool pool)
        {
            _pool = pool;
            
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
                e.playerAI.engine.SetFunction("log", new Jint.Delegates.Action<object>(Debug.Log));
                e.playerAI.engine.SetFunction("move", new Jint.Delegates.Action<string>((movement) => MovePlayer(e, movement)));
                e.playerAI.engine.SetFunction("placeBomb", new Jint.Delegates.Action(() => PlaceBomb(e)));
            }
        }
        
        private void OnGameTick()
        {
            foreach (var e in _aiPlayers.GetEntities())
            {
                // Execute each AI
                try
                {
                    e.playerAI.engine.CallFunction("OnGameTick");
                }
                catch (System.Exception exception)
                {
                    // TODO(David): Track number of errors
                    Debug.Log("Error Executing Bot: " + e.player.playerID);
                    Debug.Log(exception);
                }
            }
        }
        
        private int[] BuildAIGrid()
        {
            var width = _pool.gameBoard.width;
            var height = _pool.gameBoard.height;
            
            var grid = _pool.gameBoardCache.grid;
            var aiGrid = new int[width * height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var entity = grid[x, y];
                    int val = 0;
                    if (null == entity)
                    {
                        val = 0;
                    }
                    else if (entity.isCollideable)
                    {
                        val = 1;
                    }
                    else
                    {
                        val = 0;
                    }
                    
                    aiGrid[(x * width + y)] = val;
                }
            }
            
            return aiGrid;
        }
        
        private List<Vector2> BuildPlayers(Entity player)
        {
            List<Vector2> players = new List<Vector2>();
            foreach (var e in _aiPlayers.GetEntities())
            {
                if (e.Equals(player))
                {
                    continue;
                }
                
                players.Add(new Vector2(player.tilePosition.x, player.tilePosition.y));
            }
            
            return players;
        }
        
        private void MovePlayer(Entity entity, string movement)
        {
            // Debug.Log("Moving: " + entity.player.playerID + " " + movement);
            
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
        
        private void PlaceBomb(Entity e)
        {
            var pos = e.tilePosition;
           bool canPlaceBomb = BombLogic.CanPlaceBomb(e, pos.x, pos.y);
            if (!canPlaceBomb)
            {
                return;
            }
            
            _pool.CreateBomb(e, pos.x, pos.y, 5, 1);
        }
    }
}