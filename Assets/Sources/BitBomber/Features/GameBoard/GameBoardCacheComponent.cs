using Entitas;
using Entitas.CodeGenerator;

namespace BitBots.BitBomber.Features.GameBoard
{
    [CoreAttribute]
    [SingleEntity]
    public class GameBoardCacheComponent : IComponent
    {
        public Entity[,] grid;
    }
}