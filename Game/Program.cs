using BoardNS;
using GameNS;

namespace ProgramNS
{

    public class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();
            Game game = new Game(board);
            Console.WriteLine(board.Robot.Coordinate.X);
            Console.WriteLine(board.Robot.Coordinate.Y);
            Console.WriteLine(board.Robot.Facing);
            Console.WriteLine(board.Items[0].X);
            Console.WriteLine(board.Items[0].Y);
        }
    }
}
