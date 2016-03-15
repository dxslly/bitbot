using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;
using BitBots.BitBomber.Features.Synchronized;
using System.IO;
using BitBots.BitBomber.Assets.Sources.BitBomber.Features.Synchronized;

namespace BitBots.BitBomber
{
#if !LOGIC_ONLY
    public class SynchronizationObject : MonoBehaviour
    {

        // Use this for initialization
        public IEnumerator Start()
        {
            WebSocket w = new WebSocket(new System.Uri("ws://localhost:38681/MatchBroadcast.ashx"));
            yield return StartCoroutine(w.Connect());
            w.SendString("Hi there");
            while (true)
            {
                byte[] reply = w.Recv();
                if (reply != null)
                {
                    broadcast(reply);
                }

                if (w.error != null)
                {
                    Debug.LogError("Error: " + w.error);
                    break;
                }
                yield return 0;
            }
            w.Close();
        }
        public static void broadcast(byte[] array)
        {
            using (var stream = new MemoryStream(array))
            {
                using (var reader = new BinaryReader(stream))
                {
                    var cmd = reader.ReadInt32();
                    Entity entity = null;
                    switch (cmd)
                    {
                        case SynchronizationSystem.CREATE_ENTITY:
                            Pools.core.DeserializeEntity(reader);
                            break;
                        case SynchronizationSystem.UPDATE_ENTITY:
                            entity = Pools.core.GetEntityById(reader.ReadInt32());
                            if (entity == null)
                                Debug.LogWarning("Received broadcast: UPDATE_ENTITY entity not found");
                            else
                            {
                                var cId = 0;
                                var component = CoreComponentIds.Deserialize(reader, out cId);
                                entity.ReplaceComponent(cId, component);
                            }
                            break;
                        case SynchronizationSystem.REMOVE_ENTITY:
                            entity = Pools.core.GetEntityById(reader.ReadInt32());
                            if (entity == null)
                                Debug.LogWarning("Received broadcast: REMOVE_ENTITY entity not found");
                            else
                                entity.destroy();
                            break;
                        default:
                            Debug.LogWarning("Broadcast cmd not implemented " + cmd);
                            break;
                    }
                }
            }
        }
    }
#endif
}