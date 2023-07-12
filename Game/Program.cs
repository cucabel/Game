using BoardNS;
using CommandFactoryNS;
using GameNS;
using RobotNS;
using ValidationNS;

namespace ProgramNS
{

    public class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();
            Validation validation = new Validation();
            CommandFactory commandFactory = new CommandFactory(validation);
            Game game = new Game(board, commandFactory);
            Console.WriteLine(Robot.Instance.Coordinate.X);
            Console.WriteLine(Robot.Instance.Coordinate.Y);
            Console.WriteLine(Robot.Instance.Facing);
            foreach (var item in board.Items)
            {
                Console.WriteLine(item.X);
                Console.WriteLine(item.Y);
            }
        }
    }
}
