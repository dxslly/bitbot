using BitBots.BitBomber.Features.Synchronized;
using Entitas;
using System.Collections.Generic;
using UnityEngine;

namespace BitBots.BitBomber
{
    public static class ObjectExtensions
    {
        public static bool HasProperty(this object o, string name)
        {
            return o.GetType().GetProperty(name) != null;
        }

        public static bool HasAttribute(this object o, System.Type att)
        {
            return o.GetType().GetCustomAttributes(att, false).Length > 0;
        }

        public static bool IsSyncData(this object o)
        {
            return o.HasAttribute(typeof(IsSyncDataAttribute));
        }

        public static int GetId(this IComponent component)
        {
            for (int i = 0; i < CoreComponentIds.componentTypes.Length; ++i)
            {
                if (CoreComponentIds.componentTypes[i] == component.GetType())
                    return i;
            }
            return -1;
        }

    }
}