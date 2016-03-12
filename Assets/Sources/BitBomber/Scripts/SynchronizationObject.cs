using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;
using BitBots.BitBomber.Features.Synchronized;

namespace BitBots.BitBomber
{
    public class BroadcastMsg
    {
        public enum BroadcastMsgType { NONE, Add, Update, Delete };
        public BroadcastMsgType MsgType { get; set; }
        public int id { get; set; }
        public int componentId { get; set; }
        public Dictionary<string, object> values { get; set; }
        public EntityType EntityType { get; set; }
        public Dictionary<int, Dictionary<string, object>> components { get; set; }
    }
#if !LOGIC_ONLY
    public class SynchronizationObject : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void RemoveEntity(int id)
        {
            // TODO
            Debug.LogWarning("Received broadcast: RemoveEntity");
        }

        public void UpdateComponent(int id, int componentId, Dictionary<string, object> values)
        {
            Debug.LogWarning("Received broadcast: UpdateComponent");
            var e = Pools.core.GetEntityById(id);
            if (e == null)
                ; // TODO trace
            else
            {
                e.GetComponent(componentId).SetValues(values);
            }
        }

        private Dictionary<string, object> GetAllValues(SimpleJSON.JSONClass parsed)
        {
            var result = new Dictionary<string, object>();

            foreach (var pair in parsed.KeyValuePairs)
            {
                result[pair.Key] = pair.Value;
            }
            return result;
        }

        public void broadcast(string str)
        {
            Debug.LogWarning("Received broadcast: " + str);
            try
            {
                var obj = new BroadcastMsg();
                var parsed = SimpleJSON.JSON.Parse(str);

                obj.MsgType = (BitBots.BitBomber.BroadcastMsg.BroadcastMsgType)parsed["MsgType"].AsInt;
                obj.id = parsed["id"].AsInt;
                obj.componentId = parsed["componentId"].AsInt;
                obj.EntityType = (EntityType)parsed["EntityType"].AsInt;
                if (parsed["values"].AsObject != null)
                {
                    obj.values = GetAllValues(parsed["values"].AsObject);
                }

                if (parsed["components"].AsObject != null)
                {
                    obj.components = new Dictionary<int, Dictionary<string, object>>();

                    foreach (var pair in parsed["components"].AsObject.KeyValuePairs)
                    {
                        obj.components[int.Parse(pair.Key)] = GetAllValues(pair.Value.AsObject);
                    }
                }

                Debug.LogWarning("Parsed broadcast: MsgType: " + obj.MsgType);
                if (obj.MsgType == BroadcastMsg.BroadcastMsgType.Add)
                    AddEntity(obj.id, obj.EntityType, obj.components);
                else if (obj.MsgType == BroadcastMsg.BroadcastMsgType.Update)
                    UpdateComponent(obj.id, obj.componentId, obj.values);
                else
                    RemoveEntity(obj.id);
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Broadcast caused exception");
                Debug.LogException(ex);
            }
        }

        public void AddEntity(int id, BitBots.BitBomber.Features.Synchronized.EntityType type, Dictionary<int, Dictionary<string, object>> components)
        {
            Debug.LogWarning("Received broadcast: AddEntity");
            var emptyScript = @"function OnGameTick (ev)
    return Nothing;
end";
            Entity entity = null;
            switch (type)
            {
                case Features.Synchronized.EntityType.Player:
                    entity = Pools.core.CreateAIPlayer(1, 2, 2, Features.Player.PlayerColor.Blue, emptyScript);
                    Debug.LogWarning("AddEntity Added Player");
                    break;
                case Features.Synchronized.EntityType.Bomb:
                    entity = Pools.core.CreateBomb(null, 0, 0, 0, 0);
                    break;

                default:
                    Debug.LogWarning("AddEntity Invalid EntityType");
                    // TODO trace
                    break;
            }
            if (entity != null)
            {
                entity.synchronized.id = id;
                foreach (var c in components)
                {
                    var component = System.Activator.CreateInstance(CoreComponentIds.componentTypes[c.Key]) as IComponent;
                    component.SetValues(c.Value);
                    entity.ReplaceComponent(c.Key, component);
                }
            }
        }

    }
#endif
}