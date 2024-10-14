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
        }
    }
}
