using BitBots.BitBomber.Features.Synchronized;
using Entitas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BitBots.BitBomber.Assets.Sources.BitBomber.Features.Synchronized
{
    public class SynchronizationSystem : ISetPool, ISystem
    {
        public const int CREATE_ENTITY = 0;
        public const int UPDATE_ENTITY = 1;
        public const int REMOVE_ENTITY = 2;
        Group _group;
        public void SetPool(Pool pool)
        {
            _group = pool.GetGroup(CoreMatcher.Synchronized);

            _group.OnEntityAdded += _group_OnEntityAdded;
            _group.OnEntityRemoved += _group_OnEntityRemoved;
        }

        public delegate void BroadcastEvent(byte[] array);
        public event BroadcastEvent Broadcast;

        void _group_OnEntityRemoved(Group group, Entity entity, int index, IComponent component)
        {
            if (Broadcast != null && entity.hasSynchronized)
            {
                using (var stream = new MemoryStream())
                {
                    using (var writer = new BinaryWriter(stream))
                    {
                        writer.Write(SynchronizationSystem.REMOVE_ENTITY);
                        writer.Write(entity.synchronized.id);
                        Broadcast(stream.ToArray());
                    }
                }
            }
            entity.OnComponentAdded -= entity_OnComponentAdded;
            entity.OnComponentReplaced -= entity_OnComponentReplaced;
        }

        void _group_OnEntityAdded(Group group, Entity entity, int index, IComponent component)
        {

            if (Broadcast != null && entity.hasSynchronized)
            {
                using (var stream = new MemoryStream())
                {
                    using (var writer = new BinaryWriter(stream))
                    {
                        writer.Write(SynchronizationSystem.CREATE_ENTITY);
                        entity.Serialize(writer);
                        Broadcast(stream.ToArray());
                    }
                }
            }
            entity.OnComponentAdded += entity_OnComponentAdded;
            entity.OnComponentReplaced += entity_OnComponentReplaced;
        }

        protected void BroadcastUpdate(Entity entity, IComponent component)
        {
            if (Broadcast != null && entity.hasSynchronized && component.IsSyncData())
            {
                using (var stream = new MemoryStream())
                {
                    using (var writer = new BinaryWriter(stream))
                    {
                        writer.Write(SynchronizationSystem.UPDATE_ENTITY);
                        writer.Write(entity.synchronized.id);
                        CoreComponentIds.Serialize(component, writer);
                        Broadcast(stream.ToArray());
                    }
                }
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
