using Entitas;
using Entitas.CodeGenerator;

namespace BitBots.BitBomber.Features.GameBoard
{
    [CoreAttribute]
    [SingleEntity]
    public class GameBoard : IComponent
    {
        public int width;
        public int height;
    }
}