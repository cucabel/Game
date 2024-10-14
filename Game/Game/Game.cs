namespace ToyRobot
{
    public class Game : IGameUI
    {
        private IConsola consola;
        private IPlay board;
        private ICommandFactory commandFactory;
        public Game() { }
        public Game(IConsola consola, IPlay board, ICommandFactory commandFactory)
        {
            this.consola = consola;
            this.board = board;
            this.commandFactory = commandFactory;
            invokeCommand();
        }
        private void invokeCommand()
        {
            string message = "Introduce the commands" +
                "\nPress enter key after each command" +
                "\nPress enter key again to quit" +
                "\nAfter quitting, you can start again" +
                $"\nThe bottom left of the board is {(int)Parameters.MinWidth},{(int)Parameters.MinHeight}" +
                $"\nThe top right of the board is {(int)Parameters.MaxWidth},{(int)Parameters.MaxHeight}";

            setOutput(message);
            string input = getInput();

            while (!input.Equals(""))
            {
                setCommand(input);
                input = getInput();
            }
        }
        public void setOutput(string message)
        {
            consola.print(message);
        }
        private string getInput()
        {
            return consola.readInput();
        }
        private void setCommand(string input)
        {
            try
            {
                ICommand command = commandFactory.getCommand(input, board, this);
                if (command == null)
                {
                    setOutput("Wrong command");
                }
                else
                {
                    executeCommand(command);
                }
            }
            catch (Exception e)
            {
                setOutput(e.Message);
            }
        }
        private void executeCommand(ICommand command)
        {
            command.execute();
        }
        public IConsola Consola { get => consola; }
        public IPlay Board { get => board; }
        public ICommandFactory CommandFactory { get => commandFactory; }
    }
}
