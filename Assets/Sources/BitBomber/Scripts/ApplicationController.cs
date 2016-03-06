using BitBots.BitBomber.Features.GameBoard;
using BitBots.BitBomber.Features.Tiles;
using BitBots.BitBomber.Features.View;
using BitBots.BitBomber.Features.GameTick;
using BitBots.BitBomber.Features.PlayerAI;
using BitBots.BitBomber.Features.Movement;
using Entitas;
using Entitas.Unity.VisualDebugging;
using Jint;
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
            // Load AI From
            TextAsset blueTextAsset = Resources.Load<TextAsset>("BitBomber/PlayerAIs/SimpleAI");
            if (null == blueTextAsset)
            {
                Debug.LogWarning("Unable to load AI Script");
                return;
            }
            
            JintEngine blueEngine = new JintEngine();
            blueEngine.Run(blueTextAsset.text);
            
            // Blue Player
            pool.CreateEntity()
                .AddTilePosition(1, 1)
                .AddPlayer("Blue Player")
                .AddPlayerAI(blueEngine)
                .AddPrefab(Res.bluePlayer);
                
            TextAsset redTextAsset = Resources.Load<TextAsset>("BitBomber/PlayerAIs/RandomAI");
            if (null == redTextAsset)
            {
                Debug.LogWarning("Unable to load AI Script");
                return;
            }
                
            JintEngine redEngine = new JintEngine();
            redEngine.Run(redTextAsset.text);
            
            // Red Player
            pool.CreateEntity()
                .AddTilePosition(8, 8)
                .AddPlayer("Red Player")
                .AddPlayerAI(redEngine)
                .AddPrefab(Res.redPlayer);
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
                .Add(pool.CreateSystem<MoveSystem>());
        }
    }
}