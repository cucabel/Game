using CoordinateNS;
using FacingNS;
using ICommandFactoryNS;
using ICommandNS;
using IPlayNS;
using IValidationNS;
using MoveCommandNS;
using PlaceRobotCommandNS;
using PlaceWallCommandNS;
using ReportCommandNS;
using RobotNS;

namespace CommandFactoryNS
{
    public class CommandFactory : ICommandFactory
    {
        private IValidation validation;
        public CommandFactory(IValidation validation) 
        { 
            this.validation = validation;
        }
        public ICommand getCommand(string input, IPlay board) 
        {
            ICommand command = null;
            string[] userCommand = input.Split(' ');
            string request = userCommand[0];
            string[] location = new string[3];
            int row = 0;
            int col = 0;

            if (userCommand.Length > 1)
            {
                location = userCommand[1].Split(',');
                row = int.Parse(location[0]);
                col = int.Parse(location[1]);
            }

            if (request.Equals("PLACE_ROBOT"))
            {
                string facing = location[2];

                if (validation.validateLocation(row, col, facing))
                {
                    Coordinate validCoordinate = getCoordinate(row, col);
                    Facing validFacing = (Facing)Enum.Parse(typeof(Facing), facing);
                    command = new PlaceRobotCommand(board, validCoordinate, validFacing);
                }
            }
            else if (request.Equals("PLACE_WALL"))
            {
                if (validation.validateCoordinate(row, col))
                {
                    Coordinate validCoordinate = getCoordinate(row, col);
                    if (!validation.isOccupiedLocation(validCoordinate, board))
                    {
                        command = new PlaceWallCommand(board, validCoordinate);
                    }
                }
            } else if (request.Equals("REPORT"))
            {
                if (validation.isRobot())
                {
                    command = new ReportCommand(board);
                }
            } else if (request.Equals("MOVE"))
            {
                if (validation.isRobot())
                {
                    Coordinate nextCoordinate = board.moveOneSpaceForward(Robot.Instance.Coordinate, Robot.Instance.Facing);
                    if (!validation.isOccupiedLocation(nextCoordinate, board))
                    {
                        command = new MoveCommand(board, nextCoordinate);
                    }
                }
            }
            return command;
        }
        public Coordinate getCoordinate(int row, int col)
        {
            return new Coordinate(row, col);
        }
    }
}
