using BoardNS;
using CoordinateNS;
using FacingNS;
using ICommandNS;
using PlaceRobotCommandNS;

namespace GameNS
{
    public class Game
    {
        private Board board;
        private ICommand command;

        public Game() { }
        public Game(Board board) 
        { 
            this.board = board;
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
        public void setCommand(string input)
        {
            string[] userCommand = input.Split(' ');
            string[] location = userCommand[1].Split(',');
            int row = int.Parse(location[0]);
            int col = int.Parse(location[1]);
            string facing = location[2];

            if (validateCommand(row, col, facing))
            {
                Coordinate validCoordinate = new Coordinate(row, col);
                Facing validFacing = (Facing)Enum.Parse(typeof(Facing), facing);
                command = new PlaceRobotCommand(board, validCoordinate, validFacing);
            }
        }

        public void executeCommand() 
        { 
            if (command != null)
            {
                command.execute();
            }
        }
        public string getInput()
        {
            return Console.ReadLine();
        }

        public Boolean validateCommand(int row, int col, string facing)
        {
            if (row >= Board.MIN_HEIGHT1 &&
                row <= Board.MAX_HEIGHT1 &&
                col >= Board.MIN_WIDTH1 &&
                col <= Board.MAX_WIDTH1 &&
                Enum.IsDefined(typeof(Facing), facing))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Board Board { get => board; set => board = value; }
        public ICommand Command { get => command; set => command = value; }
    }
}
