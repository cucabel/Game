namespace ToyRobot
{
    public class Program
    {
        static void Main(string[] args)
        {
            IConsola consola = new Consola();
            IPlay board = new Board();
            IValidation validation = new Validation();
            ICommandFactory commandFactory = new CommandFactory(validation);
            Game game = new Game(consola, board, commandFactory);
            //Console.WriteLine(Robot.Instance.Coordinate.X);
            //Console.WriteLine(Robot.Instance.Coordinate.Y);
            //Console.WriteLine(Robot.Instance.Cardinal);
            //foreach (var item in board.Items)
            //{
            //    Console.WriteLine(item.X);
            //    Console.WriteLine(item.Y);
            //}
        }
    }
}
