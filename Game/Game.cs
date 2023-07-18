namespace ToyRobot
{
    public class Game
    {
        private IPlay board;
        private ICommandFactory commandFactory;
        private ICommand command;

        public Game() { }
        public Game(IPlay board, ICommandFactory commandFactory)
        {
            this.board = board;
            this.commandFactory = commandFactory;
            invokeCommand();
        }
        public void invokeCommand()
        {
            string input = getInput();

            while (!input.Equals(""))
            {
                setCommand(input);
                executeCommand();
                input = getInput();
            }
        }
        public string getInput()
        {
            return Console.ReadLine();
        }
        public void setCommand(string input)
        {
            command = commandFactory.getCommand(input, board);
        }

        public void executeCommand()
        {
            if (command != null)
            {
                command.execute();
            }
        }
        public IPlay Board { get => board; set => board = value; }
        public ICommandFactory CommandFactory { get => commandFactory; set => commandFactory = value; }
        public ICommand Command { get => command; set => command = value; }
    }
}
