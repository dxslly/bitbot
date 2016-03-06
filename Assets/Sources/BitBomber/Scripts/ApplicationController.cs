using BitBots.BitBomber.Features.GameBoard;
using BitBots.BitBomber.Features.Tiles;
using BitBots.BitBomber.Features.View;
using BitBots.BitBomber.Features.GameTick;
using BitBots.BitBomber.Features.PlayerAI;
using BitBots.BitBomber.Features.Player;
using BitBots.BitBomber.Features.Movement;
using BitBots.BitBomber.Features.Bomb;
using BitBots.BitBomber.Features.Expireable;
using BitBots.BitBomber.Features.Destroyable;
using BitBots.BitBomber.Features.Damageable;
using Entitas;
using Entitas.Unity.VisualDebugging;
using UnityEngine;

namespace BitBots.BitBomber
{
    public class ApplicationController : MonoBehaviour
    {
        private Systems _coreSystems;
        
        // Main entry point for the application
        private void Start ()
        {
            Random.seed = 42;
            _coreSystems = CreateSystems(Pools.core);
            _coreSystems.Initialize();
            
            CreateAI(Pools.core);
        }
        
        private void Update()
        {
            _coreSystems.Execute();
        }
        
        private void CreateAI(Pool pool)
        {
            pool.CreateAIPlayer(1, 1, PlayerColor.Red, "BitBomber/PlayerAIs/RandomAI");
            pool.CreateAIPlayer(8, 8, PlayerColor.Blue, "BitBomber/PlayerAIs/RandomAI");
        }
        
        private Systems CreateSystems(Pool pool)
        {
            #if (UNITY_EDITOR)
            return new DebugSystems()
            #else
            return new Systems()
            #endif
                // Create GameBoard
                .Add(pool.CreateSystem<CreateGameBoardSystem>())
                .Add(pool.CreateSystem<CreateGameBoardCacheSystem>())
            
                // Views
                .Add(pool.CreateSystem<AddPrefabSystem>())
                .Add(pool.CreateSystem<AddViewSystem>())
                .Add(pool.CreateSystem<ViewContainerSystem>())
                .Add(pool.CreateSystem<RemoveViewSystem>())
                
                // Tiles
                .Add(pool.CreateSystem<RenderTilePositionSystem>())
                
                // GameTick
                .Add(pool.CreateSystem<ExecuteGameTickSystem>())
                
                // AI
                .Add(pool.CreateSystem<ExecuteAIOnGameTickSystem>())
                
                // Movement
                .Add(pool.CreateSystem<MoveSystem>())
                
                // Expirable
                .Add(pool.CreateSystem<ExpireSystem>())
                
                // Damage
                .Add(pool.CreateSystem<DamageSystem>())
                
                // Bomb
                .Add(pool.CreateSystem<ExplodeBombSysem>())
                
                // Destroyable
                .Add(pool.CreateSystem<DestroySystem>());
        }
    }
}