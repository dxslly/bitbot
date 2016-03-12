using BitBots.BitBomber.Features.Synchronized;
using Entitas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BitBots.BitBomber.Assets.Sources.BitBomber.Features.Synchronized
{
    public class SynchronizationSystem : ISetPool, ISystem
    {
        Group _group;
        public void SetPool(Pool pool)
        {
            _group = pool.GetGroup(CoreMatcher.Synchronized);

            _group.OnEntityAdded += _group_OnEntityAdded;
            _group.OnEntityRemoved += _group_OnEntityRemoved;
        }

        public delegate void EntityAddedEvent(int id, EntityType type, Dictionary<int, Dictionary<string, object>> components);
        public event EntityAddedEvent OnEntityAdded;
        public delegate void EntityRemovedEvent(int id);
        public event EntityRemovedEvent OnEntityRemoved;
        public delegate void ComponentUpdateEvent(int id, int componentId, Dictionary<string, object> value);
        public event ComponentUpdateEvent OnComponentUpdated;

        void _group_OnEntityRemoved(Group group, Entity entity, int index, IComponent component)
        {
            if (OnEntityRemoved != null && entity.hasSynchronized)
                OnEntityRemoved(entity.synchronized.id);
            entity.OnComponentAdded -= entity_OnComponentAdded;
            entity.OnComponentReplaced -= entity_OnComponentReplaced;
        }

        void _group_OnEntityAdded(Group group, Entity entity, int index, IComponent component)
        {
            if (OnEntityAdded != null && entity.hasSynchronized)
                OnEntityAdded(entity.synchronized.id, entity.synchronized.Type, entity.GetAllSyncComponents());
            entity.OnComponentAdded += entity_OnComponentAdded;
            entity.OnComponentReplaced += entity_OnComponentReplaced;
        }

        protected void BroadcastUpdate(Entity entity, IComponent component)
        {
            if (OnComponentUpdated != null && entity.hasSynchronized && component.IsSyncData())
            {
                
                OnComponentUpdated(entity.synchronized.id, component.GetId(), component.GetValues());
            }
        }
        void entity_OnComponentReplaced(Entity entity, int index, IComponent previousComponent, IComponent newComponent)
        {
            if (newComponent.IsSyncData())
            {
                BroadcastUpdate(entity, newComponent);
            }
        }

        void entity_OnComponentAdded(Entity entity, int index, IComponent component)
        {
            if (component.IsSyncData())
            {
                BroadcastUpdate(entity, component);
            }
        }
    }
}
