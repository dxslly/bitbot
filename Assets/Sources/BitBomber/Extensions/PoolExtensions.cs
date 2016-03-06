using Entitas;
using UnityEngine;
using Jint;

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
        
        public static Entity CreateBomb(this Pool pool, Entity owner, int x, int y, int fuseTime, int spread)
        {
            return pool.CreateEntity()
                .AddBomb(fuseTime, spread)
                .AddPrefab("BitBomber/Prefabs/Bombs/DefaultBomb")
                .AddTilePosition(x, y)
                .AddOwner(owner);
        }
        
        public static Entity CreateAIPlayer(this Pool pool, int x, int y, PlayerColor color, string aiScriptPath)
        {
            // Ensure Script Exist
            TextAsset textAsset = Resources.Load<TextAsset>(aiScriptPath);
            if (null == textAsset)
            {
                Debug.LogWarning("Unable to load AI Script");
                return null;
            }
            
            // Ensure Script does not have error
            string jintErrors = "";
            if (JintEngine.HasErrors(textAsset.text, out jintErrors))
            {
                Debug.LogWarning("Bot script located at '" + aiScriptPath + "' contains the following errors:\n" + jintErrors);
                // TODO(David): For now we will continue, later do not
            }
            
            JintEngine engine = new JintEngine();
            engine.Run(textAsset.text);
            
            Entity e = pool.CreateEntity()
                .AddTilePosition(x, y)
                .AddPlayerAI(engine);
                
            string prefabPath = "NoPath";
            string name = "Name";
            switch (color)
            {
            case PlayerColor.Red:
                prefabPath = Res.redPlayer;
                name = "Red";
                break;
            case PlayerColor.Blue:
                prefabPath = Res.bluePlayer;
                name = "Blue";
                break;
            }
            
            e.AddPlayer(name);
            e.AddPrefab(prefabPath);
            
            return e;
        }
    }
}