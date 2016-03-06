using Entitas;

namespace BitBots.BitBomber.Features.Movement
{
    public static class StringExtensions
    {
        public static MoveDirection ToMoveDirection(this string s)
        {
            s = s.ToLower();
            if (s.Equals("up"))
            {
                return MoveDirection.Up;
            }
            else if (s.Equals("right"))
            {
                return MoveDirection.Right;
            }
            else if (s.Equals("down"))
            {
                return MoveDirection.Down;
            }
            else if (s.Equals("left"))
            {
                return MoveDirection.Left;
            }
            
            return MoveDirection.None;
        }
    }
}