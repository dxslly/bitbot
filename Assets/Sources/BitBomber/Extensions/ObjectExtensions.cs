using Entitas;
using System.Collections.Generic;
using UnityEngine;
using Jint.Delegates;

namespace BitBots.BitBomber
{
    public static class ObjectExtensions
    {
        public static bool HasProperty(this Object o, string name)
        {
            return o.GetType().GetProperty(name) != null;
        }
    }
}