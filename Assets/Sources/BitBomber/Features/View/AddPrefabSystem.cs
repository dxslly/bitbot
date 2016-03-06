using Entitas;
using System;
using UnityEngine;

namespace BitBots.BitBomber.Features.View
{
    public class AddPrefabSystem : IReactiveSystem, IEnsureComponents
    {
        public TriggerOnEvent trigger
        {
            get { return CoreMatcher.Prefab.OnEntityAdded(); }
        }
        
        public IMatcher ensureComponents { get { return CoreMatcher.Prefab; } }

        public void Execute(System.Collections.Generic.List<Entity> entities)
        {
            foreach (var e in entities)
            {
                GameObject prefab = Resources.Load<GameObject>(e.prefab.path);

                // Ensure that the prefab exists
                if (null == prefab)
                {
                    Debug.LogWarning("Unable to load prefab: '" + e.prefab.path + "'");

                    #if UNITY_EDITOR
                    prefab = Resources.Load<GameObject>("Common/Prefabs/Utils/MissingPrefab");
                    #else
                    continue;
                    #endif
                }


                GameObject prefabInstance = UnityEngine.Object.Instantiate<GameObject>(prefab);
                if (e.hasView)
                {
                    e.ReplaceView(prefabInstance);
                }
                else
                {
                    e.AddView(prefabInstance);
                }
            }
        }
    }
}
