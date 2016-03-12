using Entitas;
using System.Collections.Generic;
using UnityEngine;
using BitBots.BitBomber.Features.Movement;
using BitBots.BitBomber.Features.Bomb;
using System.Linq;

namespace BitBots.BitBomber.Features.PlayerAI
{
    public class ExecuteAIOnGameTickSystem : ISetPool, ISystem
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

        private void OnGameTick()
        {
            foreach (var e in _aiPlayers.GetEntities())
            {
                // Execute each AI
                try
                {
                    // TODO Timeout
                    var cmd = (int)e.playerAI.engine.Script.Call(e.playerAI.engine.Script.Globals["OnGameTick"], 1).Number;
                    ExecuteCommand(e, (Command)cmd);
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
        private void ExecuteCommand(Entity entity, Command cmd)
        {
            switch (cmd)
            {
                case Command.PlantBomb:
                    PlaceBomb(entity);
                    break;
                case Command.MoveUp:
                    MovePlayer(entity, MoveDirection.Up);
                    break;
                case Command.MoveDown:
                    MovePlayer(entity, MoveDirection.Down);
                    break;
                case Command.MoveLeft:
                    MovePlayer(entity, MoveDirection.Left);
                    break;
                case Command.MoveRight:
                    MovePlayer(entity, MoveDirection.Right);
                    break;
                case Command.Nothing:
                    // Do nothing
                    break;
                default:
                    Debug.Log("User not moving");
                    break;
            }
        }
        private void MovePlayer(Entity entity, MoveDirection moveDirection)
        {
            // Debug.Log("Moving: " + entity.player.playerID + " " + movement);

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