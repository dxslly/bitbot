using Entitas;
using UnityEngine;

using BitBots.BitBomber.Features.View;
using BitBots.BitBomber.Features.Tiles;
using BitBots.BitBomber.Features.GameBoard;
using BitBots.BitBomber.Features.Player;

namespace BitBots.BitBomber
{
    public static class PoolExtensions
    {
        static readonly string[] _walkableTiles = {
            Res.walkableTile01,
            Res.walkableTile02,
            Res.walkableTile03,
            Res.walkableTile04,
        };
        
        static readonly string[] _solidTiles = {
            Res.solidTile01,
            Res.solidTile02,
            Res.solidTile03,
            Res.solidTile04,
        };

        public static Entity CreateWalkableTile(this Pool pool, int x, int y)
        {
            return pool.CreateEntity()
                .AddTilePosition(x, y)
                .IsGameBoardElement(true)
                .AddPrefab(_walkableTiles[Random.Range(0, _walkableTiles.Length)]);
        }
        
        public static Entity CreateSolidTitle(this Pool pool, int x, int y)
        {
            return pool.CreateEntity()
                .AddTilePosition(x, y)
                .IsGameBoardElement(true)
                .IsCollideable(true)
                .AddPrefab(_solidTiles[Random.Range(0, _solidTiles.Length)]);
        }
        
        public static Entity CreatePlayer(this Pool pool, int x, int y, PlayerColor color)
        {
            Entity e = pool.CreateEntity()
                .AddTilePosition(x, y);
                
            string prefabPath = "NoPath";
            switch (color)
            {
            case PlayerColor.Red:
                prefabPath = Res.redPlayer;
                break;
            case PlayerColor.Blue:
                prefabPath = Res.bluePlayer;
                break;
            }
            
            e.AddPrefab(prefabPath);
            
            return e;
        }
    }
}