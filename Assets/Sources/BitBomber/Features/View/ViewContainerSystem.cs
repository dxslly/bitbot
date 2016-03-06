using Entitas;
using System.Collections.Generic;
using UnityEngine;

namespace BitBots.BitBomber.Features.View
{
    public class ViewContainerSystem : IReactiveSystem
    {
        private readonly Transform _viewContainer = new GameObject("Views").transform; 

        public TriggerOnEvent trigger
        {
            get
            {
                return CoreMatcher.View.OnEntityAdded();
            }
        }
        
        public void Execute(List<Entity> entities)
        {   
            foreach (var e in entities)
            {
                e.view.gameObject.transform.SetParent(_viewContainer, false);
            }
        }
    }
}