using BoardNS;
using CoordinateNS;
using FacingNS;
using IPlayNS;
using IValidationNS;
using RobotNS;

namespace ValidationNS
{
    public class Validation : IValidation
    {
        public Validation() { }

        public Boolean validateLocation(int row, int col, string facing)
        {
            Boolean validFacing = Enum.IsDefined(typeof(Facing), facing);

            if (validateCoordinate(row, col) && validFacing) return true;

            return false;
        }

        public Boolean validateCoordinate(int row, int col)
        {
            Boolean validCoordinate = row >= Board.MIN_HEIGHT1 && row <= Board.MAX_HEIGHT1 &&
                col >= Board.MIN_WIDTH1 && col <= Board.MAX_WIDTH1;

            if (validCoordinate) return true;

            return false;
        }
        public Boolean isOccupiedLocation(Coordinate coordinate, IPlay board)
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
