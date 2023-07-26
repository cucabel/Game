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
            setOutput();
            string input = getInput();            

            while (!input.Equals(""))
            {
                setCommand(input);
                executeCommand();
                input = getInput();
            }
        }
        public void setOutput()
        {
            Console.WriteLine("Introduce the commands");
            Console.WriteLine("Press enter key after each command");
            Console.WriteLine("Press enter key again to quit");
            Console.WriteLine("After quitting, you can start again");
            Console.WriteLine($"The bottom left of the board is");
            Console.WriteLine($"The top right of the board is");
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
            Console.WriteLine("Wrong command");
        }
        public IPlay Board { get => board; set => board = value; }
        public ICommandFactory CommandFactory { get => commandFactory; set => commandFactory = value; }
        public ICommand Command { get => command; set => command = value; }
    }
}
