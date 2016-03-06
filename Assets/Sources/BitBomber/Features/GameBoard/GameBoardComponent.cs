using Entitas;
using Entitas.CodeGenerator;

namespace BitBots.BitBomber.Features.GameBoard
{
    [CoreAttribute]
    [SingleEntity]
    public class GameBoardComponent : IComponent
    {
        public int width;
        public int height;
    }
}