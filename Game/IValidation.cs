using CoordinateNS;
using IPlayNS;

namespace IValidationNS
{
    public interface IValidation
    {
        public Boolean validateLocation(int row, int col, string facing);
        public Boolean validateCoordinate(int row, int col);
        public Boolean isOccupiedLocation(Coordinate coordinate, IPlay board);
        public Boolean isRobot();
    }
}
