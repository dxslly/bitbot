using Entitas;
using UnityEngine;
using BitBots.BitBomber.Features.View;
using BitBots.BitBomber.Features.Tiles;
using BitBots.BitBomber.Features.GameBoard;
using BitBots.BitBomber.Features.Player;
using BitBots.BitBomber.Features.PlayerAI;
using BitBots.BitBomber.Features.Synchronized;

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

        public static Entity GetEntityById(this Pool pool, int id)
        {
            foreach (var e in Pools.core.GetEntities(CoreMatcher.Synchronized))
            {
                if (e.synchronized.id == id)
                    return e;
            }
            return null;
        }

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

        public static Entity CreateExplosion(this Pool pool, int x, int y)
        {
            return pool.CreateEntity()
                .AddTilePosition(x, y)
                .AddDamager(1)
                .AddExpireable(3)
                .AddPrefab("BitBomber/Prefabs/Bombs/Explosion");
        }

        public static Entity CreateBomb(this Pool pool, Entity owner, int x, int y, int fuseTime, int spread)
        {
            return pool.CreateEntity()
                .AddBomb(spread)
                .AddPrefab("BitBomber/Prefabs/Bombs/DefaultBomb")
                .AddTilePosition(x, y)
                .AddExpireable(fuseTime)
                .AddOwner(owner)
                .AddSynchronized();
        }

        public static Entity CreateAIPlayer(this Pool pool, int playerId, int x, int y, PlayerColor color, string aiScript)
        {
            Entity e = pool.CreateEntity()
                .AddTilePosition(x, y)
                .IsDamageable(true)
                .AddHealth(1)
                .AddPlayer(playerId)
                .AddSynchronized();

            if (aiScript != null)
            {
                // Ensure Script does not have error
                // TODO add timeout, this will execute anything outside functions
                var engine = new BLScript();
                engine.LoadScript(aiScript);
                Debug.LogWarning("Script Loaded");
                e.AddPlayerAI(engine);
            }


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

        public static Entity DeserializeEntity(this Pool pool, System.IO.BinaryReader reader)
        {
            var id = reader.ReadInt32();
            var entity = pool.GetEntityById(id);
            if (entity == null)
                entity = pool.CreateEntity().AddSynchronized(id);
            var length = reader.ReadInt32();
            var cId = 0;
            for (int i = 0; i < length; i++)
            {
                var c = CoreComponentIds.Deserialize(reader, out cId);
                entity.ReplaceComponent(cId, c);
            }
            return entity;
        }
    }
}