namespace ToyRobot
{
    public class Game
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
        public void invokeCommand()
        {
            setOutput();
            string input = getInput();            

            while (!input.Equals(""))
            {
                setCommand(input);
                input = getInput();
            }
        }
        public void setOutput()
        {
            List<string> messages = new List<string> { 
                "Introduce the commands", 
                "Press enter key after each command", 
                "Press enter key again to quit",
                "After quitting, you can start again",
                $"The bottom left of the board is",
                $"The top right of the board is"
            };

            foreach (var message in messages)
            {
                consola.print(message);
            }
        }
        public string getInput()
        {
            return consola.readInput();
        }        
        public void setCommand(string input)
        {
            try
            {
                ICommand command = commandFactory.getCommand(input, board);
                if (command == null)
                {
                    consola.print("Wrong command");
                }
                else
                {
                    executeCommand(command);
                }
            }
            catch (Exception e)
            {
                consola.print(e.Message);
            }
        }

        public void executeCommand(ICommand command)
        {
            command.execute(); 
        }
        public IConsola Consola { get => consola; set => consola = value; }
        public IPlay Board { get => board; set => board = value; }
        public ICommandFactory CommandFactory { get => commandFactory; set => commandFactory = value; }
    }
}
