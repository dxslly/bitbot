using BitBots.BitBomber.Features.GameBoard;
using BitBots.BitBomber.Features.Tiles;
using BitBots.BitBomber.Features.View;

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
        }
        
        private void Update()
        {
            _coreSystems.Execute();
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
                .Add(pool.CreateSystem<RenderTilePositionSystem>());
        }
    }
}