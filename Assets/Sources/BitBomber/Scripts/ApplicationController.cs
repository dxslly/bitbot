using UnityEngine;
using Entitas;
using Entitas.Unity.VisualDebugging;

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
            return new DebugSystems();
            #else
            return new Systems();
            #endif
        }
    }
}