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

        public static void SetValues(this IComponent component, Dictionary<string, object> values)
        {
            try
            {
                foreach (var a in component.GetType().GetFields())
                {
                    if (values.ContainsKey(a.Name))
                    {
                        a.SetValue(component, System.Convert.ChangeType(values[a.Name], a.FieldType));
                    }
                }
            }
            catch
            {
                // TODO trace
            }
        }
        public static Dictionary<string, object> GetValues(this IComponent component)
        {
            var result = new Dictionary<string, object>();
            foreach (var a in component.GetType().GetFields())
            {
                result.Add(a.Name, a.GetValue(component));
            }
            return result;
        }
    }
}