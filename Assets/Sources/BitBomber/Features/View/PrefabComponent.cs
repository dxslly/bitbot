using Entitas;

namespace BitBots.BitBomber.Features.View
{
    [CoreAttribute]
    [BitBots.BitBomber.Features.Synchronized.IsSyncData]
    public class PrefabComponent : IComponent
    {
        public string path;
    }
}