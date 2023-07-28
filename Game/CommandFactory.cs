namespace ToyRobot
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
            string[] userCommand = input.ToUpper().Split(' ');
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

            if (request.Equals(StringCommand.PLACE_ROBOT.ToString()))
            {
                string direction = location[2];

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
            else if (request.Equals(StringCommand.PLACE_WALL.ToString()))
            {
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
            else if (request.Equals(StringCommand.REPORT.ToString()))
            {
                if (validation.isRobot())
                {
                    return new ReportCommand(board);
                }
                else throw new Exception("There is no robot on the board");
            }
            else if (request.Equals(StringCommand.MOVE.ToString()))
            {
                if (validation.isRobot())
                {
                    Coordinate nextCoordinate = board.moveOneSpaceForward(Robot.Instance.Coordinate, Robot.Instance.Cardinal);
                    if (!validation.isOccupiedCoordinate(nextCoordinate, board))
                    {
                        return new MoveCommand(board, nextCoordinate);
                    }
                    else throw new Exception("The location of the coordinate provided is occupied");
                }
                else throw new Exception("There is no robot on the board");
            }
            else if (request.Equals(StringCommand.LEFT.ToString()))
            {
                if (validation.isRobot())
                {
                    return new LeftCommand(board);
                }
                else throw new Exception("There is no robot on the board");
            }
            else if (request.Equals(StringCommand.RIGHT.ToString()))
            {
                if (validation.isRobot())
                {
                    return new RightCommand(board);
                }
                else throw new Exception("There is no robot on the board");
            }
            return null;
        }
        public Coordinate getCoordinate(int row, int col)
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
