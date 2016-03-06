using BitBots.BitBomber.Features.GameBoard;
using BitBots.BitBomber.Features.Tiles;
using BitBots.BitBomber.Features.View;
using BitBots.BitBomber.Features.GameTick;
using BitBots.BitBomber.Features.PlayerAI;
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
            TextAsset textAsset = Resources.Load<TextAsset>("BitBomber/PlayerAIs/IdleAI");
            if (null == textAsset)
            {
                Debug.LogWarning("Unable to load AI Script");
                return;
            }
            
            JintEngine blueEngine = new JintEngine();
            blueEngine.Run(textAsset.text);
            
            // Blue Player
            pool.CreateEntity()
                .AddTilePosition(0, 0)
                .AddPlayer("Blue Player")
                .AddPlayerAI(blueEngine);
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
                .Add(pool.CreateSystem<ExecuteAIOnGameTickSystem>());
        }
    }
}