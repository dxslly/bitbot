using Entitas;
using UnityEngine;

using BitBots.BitBomber.Features.View;
using BitBots.BitBomber.Features.Tiles;

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

        public static Entity CreateWalkableTile(this Pool pool, int x, int y)
        {
            return pool.CreateEntity()
                .AddTilePosition(x, y)
                .AddPrefab(_walkableTiles[Random.Range(0, _walkableTiles.Length)]);
        }
    }
}