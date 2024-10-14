namespace ToyRobot
{
    public class CommandFactory : ICommandFactory
    {
        private IValidation validation;
        public CommandFactory(IValidation validation)
        {
            this.validation = validation;
        }
        public ICommand getCommand(string input, IPlay board, IGameUI game)
        {
            string[] userCommand = input.ToUpper().Split(' ');
            string request = userCommand[0];
            string[] parameters = null;

            if (userCommand.Length > 1)
            {
                parameters = userCommand[1].Split(',');
            }

            if (request.Equals(StringCommand.PlaceRobot))
            {
                int row;
                int col;
                string direction;

                if (parameters.Length == 3)
                {
                    row = isInteger(parameters[0]);
                    col = isInteger(parameters[1]);
                    direction = parameters[2];
                }
                else throw new Exception("Wrong number of parameters");

                if (validation.validateLocation(row, col, direction))
                {
                    Coordinate validCoordinate = getCoordinate(row, col);

                    if (!validation.isOccupiedCoordinate(validCoordinate, board))
                    {
                        Direction validDirection = (Direction)Enum.Parse(typeof(Direction), direction);
                        ICardinal cardinal = getCardinal(validDirection);
                        return new PlaceRobotCommand(board, validCoordinate, cardinal);
                    }
                    else throw new Exception("The location of the coordinate provided is occupied");
                }
                else throw new Exception("Invalid coordinate or direction");
            }
            else if (request.Equals(StringCommand.PlaceWall))
            {
                int row;
                int col;

                if (parameters.Length == 2)
                {
                    row = isInteger(parameters[0]);
                    col = isInteger(parameters[1]);
                }
                else throw new Exception("Wrong number of parameters");

                if (validation.validateCoordinate(row, col))
                {
                    Coordinate validCoordinate = getCoordinate(row, col);
                    if (!validation.isOccupiedCoordinate(validCoordinate, board))
                    {
                        return new PlaceWallCommand(board, validCoordinate);
                    }
                    else throw new Exception("The location of the coordinate provided is occupied");
                }
                else throw new Exception("Invalid coordinate");
            }
            else if (request.Equals(StringCommand.Report))
            {
                if (validation.isRobot(board))
                {
                    return new ReportCommand(board, game);
                }
                else throw new Exception("There is no robot on the board");
            }
            else if (request.Equals(StringCommand.Move))
            {
                if (validation.isRobot(board))
                {
                    Coordinate nextCoordinate = board.moveOneSpaceForward(board.Robot.Coordinate, board.Robot.Cardinal);
                    if (!validation.isOccupiedCoordinate(nextCoordinate, board))
                    {
                        return new MoveCommand(board, nextCoordinate, board.Robot.Cardinal);
                    }
                    else throw new Exception("The location of the coordinate provided is occupied");
                }
                else throw new Exception("There is no robot on the board");
            }
            else if (request.Equals(StringCommand.Left))
            {
                if (validation.isRobot(board))
                {
                    return new LeftCommand(board);
                }
                else throw new Exception("There is no robot on the board");
            }
            else if (request.Equals(StringCommand.Right))
            {
                if (validation.isRobot(board))
                {
                    return new RightCommand(board);
                }
                else throw new Exception("There is no robot on the board");
            }
            return null;
        }
        private int isInteger(string parameter)
        {
            if (int.TryParse(parameter, out int parameterValue))
            {
                return parameterValue;
            }
            else throw new Exception("Row and col must be numbers");
        }
        private Coordinate getCoordinate(int row, int col)
        {
            return new Coordinate(row, col);
        }
        private ICardinal getCardinal(Direction direction)
        {
            if (direction == Direction.NORTH) { return new North(); }
            if (direction == Direction.EAST) { return new East(); }
            if (direction == Direction.SOUTH) { return new South(); }
            return new West();
        }
    }
}
