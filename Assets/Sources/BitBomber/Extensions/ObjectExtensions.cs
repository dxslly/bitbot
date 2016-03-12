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

#if !LOGIC_ONLY
        public static void SetValues(this IComponent component, Dictionary<string, object> values)
        {
            try
            {
                foreach (var a in component.GetType().GetFields())
                {
                    if (values.ContainsKey(a.Name))
                    {
                        var val = System.Convert.ChangeType((values[a.Name] as SimpleJSON.JSONData).Value, a.FieldType);
                        //Debug.LogWarningFormat("Set component {0} field {2} to {1}", component.GetType().Name, val, a.Name);
                        a.SetValue(component, System.Convert.ChangeType(val, a.FieldType));
                    }
                }
            }
            catch(System.Exception ex)
            {
                // TODO trace
                Debug.LogError("SetValues threw an exception");
                Debug.Log(ex);
            }
        }
#endif
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