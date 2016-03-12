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
using BitBots.BitBomber.Assets.Sources.BitBomber.Features.Synchronized;

namespace BitBots.BitBomber
{
    public class ApplicationController
#if !LOGIC_ONLY
 : MonoBehaviour
#endif
    {
        private Systems _coreSystems;

        // Main entry point for the application
        public void Start()
        {
            _coreSystems = CreateSystems(Pools.core);
            _coreSystems.Initialize();
#if !LOGIC_ONLY
            Random.seed = 42;
#else
            CreateAI(Pools.core);
#endif
        }
#if !LOGIC_ONLY
        public void Update()
        {
#else
        public void Update(System.TimeSpan span)
        {
            Time.deltaTime = span.TotalSeconds;
#endif
            _coreSystems.Execute();
        }

        public void Reset()
        {
            foreach (var e in Pools.core.GetEntities(CoreMatcher.Synchronized))
                _synchronizationSystem.RemoveEntity(e);
            _coreSystems.DeactivateReactiveSystems();
            Pools.core.Reset();
            _coreSystems = null;
            _synchronizationSystem = null;
        }

        public int? GetOutcome()
        {
            var players = Pools.core.GetGroup(CoreMatcher.Player);
            if (players.count == 1)
            {
                return players.GetSingleEntity().player.playerID;
            }
            else if (players.count == 0)
                return 0;
            else
                return null;
        }

        private SynchronizationSystem _synchronizationSystem;
        public SynchronizationSystem SynchronizationSystem
        {
            get
            {
                if (_synchronizationSystem == null)
                {
                    _synchronizationSystem = Pools.core.CreateSystem<SynchronizationSystem>() as SynchronizationSystem;
                }
                return _synchronizationSystem;
            }
        }

        private void CreateAI(Pool pool)
        {
            var randomScript = @"function OnGameTick (ev)
                moveChance = math.random();
    
                if(moveChance < 0.1) then
		            return PlantBomb;
	            elseif (moveChance < 0.3) then
		            return MoveUp;
                elseif (moveChance < 0.6) then
		            return MoveRight;
                elseif (moveChance < 0.8) then
		            return MoveDown;
                else
		            return MoveLeft;
                end
            end";
            pool.CreateAIPlayer(1, 1, 1, PlayerColor.Red, randomScript);
            pool.CreateAIPlayer(2, 8, 8, PlayerColor.Blue, randomScript);
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

#if !LOGIC_ONLY
                // Views
                .Add(pool.CreateSystem<AddPrefabSystem>())
                .Add(pool.CreateSystem<AddViewSystem>())
                .Add(pool.CreateSystem<ViewContainerSystem>())
                .Add(pool.CreateSystem<RemoveViewSystem>())
                // Tiles
                .Add(pool.CreateSystem<RenderTilePositionSystem>())
#else
                // Synchronization System (Server)
                .Add(SynchronizationSystem)
#endif


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