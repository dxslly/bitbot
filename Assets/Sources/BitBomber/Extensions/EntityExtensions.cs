using BitBots.BitBomber.Features.Synchronized;
using Entitas;
using System.Collections.Generic;
namespace BitBots.BitBomber
{
    public static class EntityExtension
    {
        public static Entity AddSynchronized(this Entity entity, EntityType type)
        {
            return entity.AddSynchronized(BitBots.BitBomber.Features.Synchronized.SynchronizedComponent.NextId++, type);
        }

        public static Dictionary<int, Dictionary<string, object>> GetAllSyncComponents(this Entity entity)
        {

            var list = new Dictionary<int, Dictionary<string, object>>();
            var components = entity.GetComponentIndices();
            for(int i =0;i<components.Length;++i)
            {
                if (entity.GetComponent(components[i]).IsSyncData())
                {
                    list.Add(components[i], entity.GetComponent(components[i]).GetValues());
                }
            }
            return list;
        }
    }
}
