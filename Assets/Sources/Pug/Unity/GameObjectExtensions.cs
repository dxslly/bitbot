using UnityEngine;

namespace Pug.Unity
{
    public static class GameObjectExtensions
    {
        public static T GetOrAddComponent<T>(this GameObject go) where T : Component
        {
            T co = go.GetComponent<T>();
            if (null != co)
            {
                return co;
            }
            
            return go.AddComponent<T>();
        }
    }
}