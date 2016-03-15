using BitBots.BitBomber.Features.Synchronized;
using Entitas;
using System.Collections.Generic;
namespace BitBots.BitBomber
{
    public static class EntityExtension
    {
        public static Entity AddSynchronized(this Entity entity)
        {
            return entity.AddSynchronized(BitBots.BitBomber.Features.Synchronized.SynchronizedComponent.NextId++);
        }

        public static void Serialize(this Entity entity, System.IO.BinaryWriter writer)
        {
            writer.Write(entity.synchronized.id);
            var components = entity.GetComponents();
            writer.Write(components.Length);
            foreach (var c in components)
            {
                CoreComponentIds.Serialize(c, writer);
            }
        }
    }
}
