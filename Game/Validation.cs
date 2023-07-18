namespace ToyRobot
{
    public class Validation : IValidation
    {
        public Validation() { }

        public Boolean validateLocation(int row, int col, string direction)
        {
            Boolean validDirection = Enum.IsDefined(typeof(Direction), direction);

            if (validateCoordinate(row, col) && validDirection) return true;

            return false;
        }

        public Boolean validateCoordinate(int row, int col)
        {
            Boolean validCoordinate = row >= Board.MIN_HEIGHT1 && row <= Board.MAX_HEIGHT1 &&
                col >= Board.MIN_WIDTH1 && col <= Board.MAX_WIDTH1;

            if (validCoordinate) return true;

            return false;
        }
        public Boolean isOccupiedCoordinate(Coordinate coordinate, IPlay board)
        {
            if (board.Items.Contains(coordinate)) return true;

            return false;
        }
        public Boolean isRobot()
        {
            if (Robot.Instance != null) return true;

            return false;
        }
    }
}
