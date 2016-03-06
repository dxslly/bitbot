using Entitas;
using System.Collections.Generic;
using UnityEngine;

namespace BitBots.BitBomber.Features.View
{
    public class AddViewSystem : IReactiveSystem
    {
        public TriggerOnEvent trigger
        {
            get { return CoreMatcher.View.OnEntityAdded(); }
        }
        
        public void Execute(List<Entity> entities)
        {
            foreach (var e in entities)
            {
                // Ensure does not have a prefab
                if (null == e.view.gameObject)
                {
                    GameObject go = new GameObject();
                    e.view.gameObject = go;
                }

                e.view.gameObject.name = "Entity (" + e.creationIndex + ") View";
            }
        }
        
    }
}