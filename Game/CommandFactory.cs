using CoordinateNS;
using FacingNS;
using ICommandFactoryNS;
using ICommandNS;
using IPlayNS;
using IValidationNS;
using PlaceRobotCommandNS;
using PlaceWallCommandNS;

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
            string[] location = userCommand[1].Split(',');
            int row = int.Parse(location[0]);
            int col = int.Parse(location[1]);

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
            }
            return command;
        }
        public Coordinate getCoordinate(int row, int col)
        {
            return new Coordinate(row, col);
        }
    }
}
