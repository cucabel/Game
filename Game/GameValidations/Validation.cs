namespace ToyRobot
{
    public class Validation : IValidation
    {
        public Validation() { }

        public bool validateLocation(int row, int col, string direction)
        {
            bool validDirection = Enum.IsDefined(typeof(Direction), direction);

            if (validateCoordinate(row, col) && validDirection) return true;

            return false;
        }

        public bool validateCoordinate(int row, int col)
        {
            bool validCoordinate = row >= (int)Parameters.MinHeight && row <= (int)Parameters.MaxHeight &&
                col >= (int)Parameters.MinWidth && col <= (int)Parameters.MaxWidth;

            if (validCoordinate) return true;

            return false;
        }
        public bool isOccupiedCoordinate(Coordinate coordinate, IPlay board)
        {
            if (board.Items.Contains(coordinate)) return true;

            return false;
        }
        public bool isRobot(IPlay board)
        {
            if (board.Robot != null) return true;

            return false;
        }
    }
}
